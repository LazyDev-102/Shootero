using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialModeWaveData", menuName = "Resource/WaveData/MaterialMode/BasicWave")]
[System.Serializable]
public class MaterialModeWaveData : ScriptableObject {

    [SerializeField] private float waveMultipler;
    [SerializeField] private WaveCondition[] preStartCondition;
    [SerializeField] private WaveCondition[] preEndCondition;
    [SerializeField] private MaterialWaveObstacle[] obstacles;
    [SerializeField] private RangeIntValue rangeChip;
    [SerializeField] private RangeIntValue rangeHealOrb;
    [SerializeField] private RangeIntValue rangeMaterial;
    [SerializeField] private RangeIntValue rangeGear;
    [SerializeField] private int[] enemyIds;
    [SerializeField] private int[] trapIds;
    [SerializeField] private int[] minibossIds;
    [SerializeField] private int[] bossIds;
    [SerializeField] private bool isMinibossWave;
    [SerializeField] private bool isBossWave;
    [SerializeField] private DifficultSpawnEnemy difficultPercents;
    [SerializeField] private DifficultSpawnTrap trapDifficultPercents;
    [SerializeField] private ConfigModeWave configModeWaves;
    public float WaveMultipler { get => waveMultipler; set => waveMultipler = value; }
    public WaveCondition[] PreStartCondition { get => preStartCondition; }
    public WaveCondition[] PreEndCondition { get => preEndCondition; }
    public MaterialWaveObstacle[] Obstacles { get => obstacles; }
    public RangeIntValue RangeChip { get => rangeChip; }
    public RangeIntValue RangeHealOrb { get => rangeHealOrb; }
    public RangeIntValue RangeMaterial { get => rangeMaterial; }
    public RangeIntValue RangeGear { get => rangeGear; }
    public int[] EnemyIds { get => enemyIds; }
    public int[] TrapIds { get => trapIds; }
    public int[] MinibossIds { get => minibossIds; }
    public int[] BossIds { get => bossIds; }
    public bool IsMinibossWave { get => isMinibossWave; }
    public bool IsBossWave { get => isBossWave; }
    public DifficultSpawnEnemy DifficultPercens { get => difficultPercents; }
    public DifficultSpawnTrap TrapDifficultPercents { get => trapDifficultPercents; }
    public ConfigModeWave ConfigModeWaves { get => configModeWaves; }

    public virtual MaterialModeWaveInfo CreateInfo() {
        MaterialModeWaveInfo waveInfo = new MaterialModeWaveInfo();
        waveInfo.CreateData(this);
        return waveInfo;
    }
}
[System.Serializable]
public class MaterialWaveObstacle {
    [SerializeField] private MaterialModeBuffType buffType;
    [SerializeField] private MaterialModeBuffShape buffShape;
    [SerializeField] private MaterialModeBuffSize buffSize;
    [SerializeField] private Area[] spawnArea;
    [SerializeField] private ModesAction action;
    [SerializeField] private bool isLimit;
    [SerializeField] private string description;
    [SerializeField] private RangeIntValue count;

    public MaterialModeBuffType BuffType { get => buffType; }
    public MaterialModeBuffShape BuffShape { get => buffShape; }
    public MaterialModeBuffSize BuffSize { get => buffSize; }
    public Area[] SpawnArea { get => spawnArea; }
    public bool IsLimit { get => isLimit; }
    public string Description { get => description; }
    public RangeIntValue Count { get => count; }
    public bool IsBuff => buffType != MaterialModeBuffType.DecreaseAttack && buffType != MaterialModeBuffType.HitDmgLaser && buffType != MaterialModeBuffType.DecreaseAttackSpeed;

    public void InitData(ObstacleBase obstacle) {
        obstacle.SetImmortalState(buffType == MaterialModeBuffType.Immotal, 10);
    }

    public void Active(ObstacleBase obstacle) {
        action?.Execute(obstacle);
    }
    public void Deactive(ObstacleBase obstacle) {
        action?.RemoveExecute(obstacle);
    }
}
public enum MaterialModeBuffType {
    IncreaseAttack = 0,
    Healing = 1,
    CritRate = 2,
    CritDmg = 3,
    AttackSpeed = 4,
    Immotal = 5,
    HitDmgLaser = 6,
    DecreaseAttack = 7,
    DecreaseAttackSpeed = 8,
}
public enum MaterialModeBuffSize {
    Small = 0,
    Medium = 1,
    Large = 2,
    Extra = 3,
}
public enum MaterialModeBuffShape {
    Circle = 0,
    Box = 1,
}