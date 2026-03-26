using UnityEngine;

[System.Serializable]
public class GearModeWaveData {

    [SerializeField] private RangeIntValue limitRange;
    [SerializeField] private int[] enemyIds;
    [SerializeField] private int[] trapIds;
    [SerializeField] private int[] bossIds;
    [SerializeField] private int[] mbIds;
    [SerializeField] private EnemyTierData tier;
    [SerializeField] private DifficultTierData difficultTier;

    public RangeIntValue LimitRange { get => limitRange; set => limitRange = value; }
    public int[] EnemyIds { get => enemyIds; set => enemyIds = value; }
    public int[] TrapIds { get => trapIds; set => trapIds = value; }

    public int[] BossIds { get => bossIds; set => bossIds = value; }
    public int[] MbIds { get => mbIds; set => mbIds = value; }
    public DifficultTierData DifficultTier { get => difficultTier; }

    public void ChooseEnemy() {
        enemyIds = tier.RandomEnemy();
        ChangeLimitRange();
    }
    private void ChangeLimitRange() {
        RangeIntValue temp = limitRange;
        temp.startValue = temp.startValue * 150 / 100;
        temp.endValue = temp.endValue * 150 / 100;
        if (limitRange.startValue > 18)
            limitRange.endValue = 18;
        if (limitRange.endValue > 21)
            limitRange.endValue = 21;
    }
    public void Preload() {
        int currentZone = GameResources.Instance.ConquerorData.CurrentZoneIndex;
        GameResources.Instance.EnemyData.PreloadEnemies(currentZone, enemyIds)
                                        .PreloadBoss(bossIds, 1)
                                        .PreloadMiniboss(mbIds, 1)
                                        .PreloadTrap(trapIds, 3);
    }
}