

public class BossHealth : EnemyHealth {
    private BossBase bossBase;
    public BossBase BossBase {
        get {
            if (bossBase == null) {
                bossBase = CharacterBase as BossBase;
            }
            return bossBase;
        }
    }
    public override void HPReduce(int hp) {
        base.HPReduce(hp);
        if (hp > 0) {
            BossBase.CheckPhase();
        }
        Gemmob.EventDispatcher.Instance.Dispatch<EventKey.OnBossHpChanged>(new EventKey.OnBossHpChanged() { Percent = GetPercentHPRemain() });
    }
}
