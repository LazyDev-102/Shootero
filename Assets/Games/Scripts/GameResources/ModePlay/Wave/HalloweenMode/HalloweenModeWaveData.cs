using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenModeWaveData", menuName = "Resource/WaveData/Halloween/HalloweenWave")]
public class HalloweenModeWaveData : ConquerorWaveData { // hardData
    [SerializeField] private RangeIntValue rangeHItem;
    [SerializeField] private int[] enemyIds;
    [SerializeField] private int[] trapIds;
    [SerializeField] private int[] minibossIds;
    [SerializeField] private int[] bossIds;
    [SerializeField] private ConfigModeWave configModeWaves = new ConfigModeWave() { LimitRange = new RangeIntValue() { startValue = 6, endValue = 8 }, TimeRange = new RangeIntValue() { startValue = 15, endValue = 20 } };
    [SerializeField] private DifficultSpawnEnemy difficultPercents;
    [SerializeField] private DifficultSpawnTrap trapDifficultPercents;


    public ConfigModeWave ConfigModeWaves { get => configModeWaves; }
    public DifficultSpawnEnemy DifficultPercens { get => difficultPercents; }
    public DifficultSpawnTrap TrapDifficultPercents { get => trapDifficultPercents; }
    public RangeIntValue RangeHItem { get => rangeHItem; }
    public int[] EnemyIds { get => enemyIds; }
    public int[] TrapIds { get => trapIds; }
    public int[] MinibossIds { get => minibossIds; }
    public int[] BossIds { get => bossIds; }
    public bool IsMinibossWave { get => WaveType == WaveType.Miniboss; }
    public bool IsBossWave { get => WaveType == WaveType.Boss; }
    public bool IsTrapWave { get => WaveType == WaveType.Trap; }
    public bool IsEnemyWave { get => WaveType == WaveType.Normal; }

    public override ConquerorWaveInfo CreateInfo(int currentZoneIndex, int currentWaveIndex) {
        ConquerorWaveInfo waveInfo = new HalloweenModeWaveInfo();
        waveInfo.CreateData(currentZoneIndex, currentWaveIndex, this);
        return waveInfo;
    }
}