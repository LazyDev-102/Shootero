

public abstract class BossSkillBulletAttackComponent : BossSkillAttackComponent {
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(GetBossAttack().BossBase.BossStat.Atk.Value, null, GetBossAttack().BossBase);
        //foreach(var mod in ) {
        //  mod.ChangeBullet(bulletChanged);
        //}

        return bullet;
    }
}
