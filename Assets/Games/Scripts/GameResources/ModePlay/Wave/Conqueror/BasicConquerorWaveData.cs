using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicConquerorWaveData", menuName = "Resource/WaveData/Conqueror/Basic")]
public class BasicConquerorWaveData : ConquerorWaveData { // hardData

    [SerializeField] private int[] enemyIds;
    [SerializeField] private int[] trapIds;
    [SerializeField] private SetEventWaveData[] setEventRules;
    #region Limits
    [SerializeField]
    private ConfigModeWave configModeWaves = new ConfigModeWave() { LimitRange = new RangeIntValue() { startValue = 6, endValue = 8 }, TimeRange = new RangeIntValue() { startValue = 15, endValue = 20 } };
    [SerializeField] private DifficultSpawnEnemy difficultPercents;
    [SerializeField] private DifficultSpawnTrap trapDifficultPercents;
    public ConfigModeWave ConfigModeWaves { get => configModeWaves; }
    public DifficultSpawnEnemy DifficultPercens { get => difficultPercents; }
    public DifficultSpawnTrap TrapDifficultPercents { get => trapDifficultPercents; }
    #endregion

    public int[] EnemyIds { get => enemyIds; }
    public int[] TrapIds { get => trapIds; }
    public SetEventWaveData[] SetEventRules { get => setEventRules; }

    public override ConquerorWaveInfo CreateInfo(int currentZoneIndex, int currentWaveIndex) {
        BasicConquerorWaveInfo waveInfo = new BasicConquerorWaveInfo();
        waveInfo.CreateData(currentZoneIndex, currentWaveIndex, this);
        return waveInfo;
    }
}

[Serializable]
public class ConfigModeWave {
    [SerializeField] private RangeIntValue limitRange;
    [SerializeField] private RangeIntValue timeRange;

    public RangeIntValue LimitRange { get => limitRange; set => limitRange = value; }
    public RangeIntValue TimeRange { get => timeRange; set => timeRange = value; }
}
