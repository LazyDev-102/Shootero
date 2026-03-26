using Helper;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InfinityModeData", menuName = "Resource/Modes/Infinity/InfinityModeData")]
public class InfinityModeData : ScriptableObject {
    [SerializeField] private ItemStack energyNeed;
    [SerializeField] private InfinityWaveData newInfinityWaveData;
    [SerializeField] private DifficultTierData difficultTier;

    public ItemStack EnergyNeed { get => energyNeed; }

    public InfinityWavaInfo GenerateWave(InfinityWavaInfo preWave) {
        if (preWave == null) {
            preWave = new InfinityWavaInfo();
        }
        InfinityWaveData infinityData = null;
        infinityData = newInfinityWaveData;
        newInfinityWaveData.ChooseEnemy();
        preWave.CreateData(infinityData);
        return preWave;
    }

    public EnemyType GetEnemyTypeSpawn(int index) {
        if (index < 0)
            index = 0;
        if (index >= difficultTier.EnemyPercent.Length)
            index = difficultTier.EnemyPercent.Length - 1;
        return RandomHelper.RandomWithPercent(difficultTier.EnemyPercent[index].TypePercents).Type;
    }

    public EnemyType GetTrapTypeSpawn(int index) {
        if (index < 0)
            index = 0;
        if (index >= difficultTier.TrapPercent.Length)
            index = difficultTier.TrapPercent.Length - 1;
        return RandomHelper.RandomWithPercent(difficultTier.TrapPercent[index].TypePercents).Type;
    }
    //public EnemyType GetEnemyTypeSpawn(float im) {
    //    DifficultRangeInfinity curDifficult = null;
    //    foreach (var d in difficultRanges) {
    //        if (im >= d.InfinityMultiRange.startValue && im < d.InfinityMultiRange.endValue) {
    //            curDifficult = d;
    //            break;
    //        }
    //    }
    //    return RandomHelper.RandomWithPercent(curDifficult.EnemyPercent.TypePercents).Type;
    //}

    //public EnemyType GetTrapTypeSpawn(float im) {
    //    DifficultRangeInfinity curDifficult = null;
    //    foreach (var d in difficultRanges) {
    //        if (im >= d.InfinityMultiRange.startValue && im < d.InfinityMultiRange.endValue) {
    //            curDifficult = d;
    //            break;
    //        }
    //    }
    //    return RandomHelper.RandomWithPercent(curDifficult.EnemyPercent.TypePercents).Type;
    //}
    public void Preload() {
        newInfinityWaveData.Preload();
    }
    [Serializable]
    public class DifficultRangeInfinity {
        [SerializeField] private RangeFloatValue infinityMultiRange;
        [SerializeField] private DifficultSpawnEnemy enemyPercent;
        [SerializeField] private DifficultSpawnTrap trapPercent;


        public RangeFloatValue InfinityMultiRange { get => infinityMultiRange; set => infinityMultiRange = value; }
        public DifficultSpawnEnemy EnemyPercent { get => enemyPercent; set => enemyPercent = value; }
        public DifficultSpawnTrap TrapPercent { get => trapPercent; set => trapPercent = value; }

    }
}
