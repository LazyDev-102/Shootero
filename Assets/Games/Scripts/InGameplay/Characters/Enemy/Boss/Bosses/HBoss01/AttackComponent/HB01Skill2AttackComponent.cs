using UnityEngine;
using System;
using System.Collections.Generic;
using Gemmob;
using System.Collections;

public class HB01Skill2AttackComponent : BossSkillAttackComponent {
    [SerializeField] private HB01Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PierceFrontBullet bullet;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;

    private bool canAim;
    private AttackData attackData;
    private List<PierceFrontBullet> bullets = new List<PierceFrontBullet>();

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        if (bullets != null)
            bullets.Clear();
        atk = (int)(bossAttack.HB01Base.HB01Stat.Atk.Value * attackData.DamagePercent);
        canAim = true;
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Shot());
    }

    private IEnumerator Shot() {
        var directionShot = bossAttack.Target.position - transform.position;
        for (int i = 0; i < attackData.AttackCount; i++) {
            for (int ibullet = 0; ibullet < attackData.BulletCount; ++ibullet) {
                Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, attackData.Distance.GetRandomValue());
                PierceFrontBullet bulletClone = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (bulletClone) {
                    bulletClone = ChangingBullet(bulletClone);
                    bulletClone.SetSize(bossAttack.HB01Base.HB01Stat.Size.Value);
                    bulletClone.Shoot(attackData.SpeedBullet.GetRandomValue(), directionRandom);
                }
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        EndAttack();
    }

    public override void EndAttack() {
        base.EndAttack();
    }

    private int atk;
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor((int)(atk * attackData.DamagePercent), null, bossAttack.BossBase);
        return bullet;
    }
    public override void StopAttack() {
        base.StopAttack();
    }

    public override void Updating() {
        if (canAim) {
            bossAttack.HB01Base.LookTarget();
        }
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaShotTime;
        [SerializeField] private int bulletCount;
        [SerializeField] private int attackCount;
        [SerializeField] private int spreadAngle;
        [SerializeField] private RangeFloatValue speedBullet;
        [SerializeField] private RangeFloatValue distance;

        public float DamagePercent => damagePercent;
        public float DeltaShot => deltaShotTime;
        public int BulletCount => bulletCount;
        public int AttackCount => attackCount;
        public int SpreadAngle => spreadAngle;
        public RangeFloatValue SpeedBullet => speedBullet;
        public RangeFloatValue Distance => distance;
    }
}
