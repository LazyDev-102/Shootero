using Helper;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusWaveData", menuName = "Resource/WaveData/Conqueror/Bonus")]
public class BonusWaveData : ConquerorWaveData { // hardData
    [SerializeField] private RangeIntValue chestCount;
    [SerializeField] private RangeIntValue waveTime;
    [SerializeField] private RangeIntValue maxChestInCamera;
    [SerializeField] private int[] chestTypeRate;
    [SerializeField] private float[] chipPercent;
    public RangeIntValue ChestCount { get => chestCount; }
    public RangeIntValue WaveTime { get => waveTime; }
    public RangeIntValue MaxChestInCamera { get => maxChestInCamera; }

    public override ConquerorWaveInfo CreateInfo(int currentZoneIndex, int currentWaveIndex) {
        BonusWaveInfo waveInfo = new BonusWaveInfo();
        waveInfo.CreateData(currentZoneIndex, currentWaveIndex, this);
        return waveInfo;
    }
    public ChestBase GetChest() {
        var ran = RandomHelper.RandomWithPercent(chestTypeRate);
        return GameResources.Instance.EnemyData.GetChest(ran);
    }
    public int GetChip(EnemyType eType, int numberChip) {
        if (eType == EnemyType.Champion)
            return (chipPercent[2] * RangeChip.endValue / numberChip).ConvertToInt();
        else if (eType == EnemyType.Elite)
            return (chipPercent[1] * RangeChip.endValue / numberChip).ConvertToInt();
        else
            return (chipPercent[0] * RangeChip.endValue / numberChip).ConvertToInt();
    }
}

