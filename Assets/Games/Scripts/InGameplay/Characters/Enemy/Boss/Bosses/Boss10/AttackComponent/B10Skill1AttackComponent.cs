using System;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B10Skill1AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B10Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LightningBallHomingBullet bullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private float delayAfterAttack;
    [SerializeField] private int numberPreload;

    private AttackData attackData;

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
    }

    public override void Updating() {
        bossAttack.B10Base.LookTarget();
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        if (charge) {
            charge.Play();
        }
        yield return Yielder.Wait(delayAttack);
        Transform target = bossAttack.Target;
        float bulletSpeed = attackData.BulletSpeed;
        float bulletAcceler = attackData.BulletAcceler;
        Vector2 directionShot = firePoint.up;
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            if (muzzle) {
                muzzle.Play();
            }
            LightningBallHomingBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.SetDelayHoming(attackData.DelayHoming);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                newBullet.SetLifeTimeHoming(attackData.LifeTime);
                newBullet.Shoot(bulletSpeed, target, directionShot, bulletAcceler);
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        yield return Yielder.Wait(delayAfterAttack);
        EndAttack();
    }



    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaShot;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletAcceler;
        [SerializeField] private float delayHoming;
        [SerializeField] private float lifeTime;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public float BulletSpeed { get => bulletSpeed; }
        public float LifeTime { get => lifeTime; }
        public float BulletAcceler { get => bulletAcceler; }
        public float DelayHoming { get => delayHoming; }
    }
}
