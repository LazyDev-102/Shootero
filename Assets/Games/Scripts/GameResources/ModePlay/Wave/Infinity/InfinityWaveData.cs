using Gemmob;
using System;
using UnityEngine;

[Serializable]
public class InfinityWaveData {
    [SerializeField] private RangeIntValue limitRange;
    [SerializeField] private int[] enemyIds;
    [SerializeField] private int[] trapIds;
    [SerializeField] private int[] bossIds;
    [SerializeField] private int[] mbIds;

    public RangeIntValue LimitRange { get => limitRange; set => limitRange = value; }
    public int[] EnemyIds { get => enemyIds; set => enemyIds = value; }
    public int[] TrapIds { get => trapIds; set => trapIds = value; }

    public int[] BossIds { get => bossIds; set => bossIds = value; }
    public int[] MbIds { get => mbIds; set => mbIds = value; }

    [SerializeField] private EnemyTierData tier;

    public void ChooseEnemy() {
        enemyIds = tier.RandomEnemy();
        ChangeLimitRange();
    }
    private void ChangeLimitRange() {
        RangeIntValue temp = limitRange;
        temp.startValue = temp.startValue * 110 / 100;
        temp.endValue = temp.endValue * 110 / 100;
        if (limitRange.startValue > 15)
            limitRange.endValue = 15;
        if (limitRange.endValue > 20)
            limitRange.endValue = 20;
    }
    public void Preload() {
        int currentZone = GameResources.Instance.ConquerorData.CurrentZoneIndex;
        GameResources.Instance.EnemyData.PreloadEnemies(currentZone, enemyIds)
                                        .PreloadBoss(bossIds, 1)
                                        .PreloadMiniboss(mbIds, 1)
                                        .PreloadTrap(trapIds, 3);
    }
}
