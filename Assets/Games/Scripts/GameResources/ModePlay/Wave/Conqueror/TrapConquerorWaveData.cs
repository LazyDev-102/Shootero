using Helper;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TrapConquerorWaveData", menuName = "Resource/WaveData/Conqueror/Trap")]
public class TrapConquerorWaveData : ConquerorWaveData { // hardData
    [SerializeField] private RangeIntValue waveTime;
    [SerializeField] private DifficultSpawnTrap trapDifficultPercents;
    [SerializeField] private int[] trapIds;
    [SerializeField] private float deltaTime;
    public RangeIntValue WaveTime { get => waveTime; }
    public float DeltaTime { get => deltaTime; }

    public override ConquerorWaveInfo CreateInfo(int currentZoneIndex, int currentWaveIndex) {
        TrapConquerorWaveInfo waveInfo = new TrapConquerorWaveInfo();
        waveInfo.CreateData(currentZoneIndex, currentWaveIndex, this);
        return waveInfo;
    }
    public TrapBase GetTrap() {
        TypeEnemyPercent randomType = RandomHelper.RandomWithPercent(trapDifficultPercents.TypePercents);
        EnemyType type = randomType.Type;
        return GameResources.Instance.EnemyData.GetTrapRandom(trapIds, type);
    }
}