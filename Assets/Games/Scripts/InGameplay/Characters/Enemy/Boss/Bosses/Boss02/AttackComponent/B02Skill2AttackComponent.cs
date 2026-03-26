using Helper;
using System;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B02Skill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B02Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
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
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        if (charge) {
            charge.Play();
        }
        yield return Yielder.Wait(delayAttack);
        float bulletSpeed = attackData.BulletSpeed;
        float bulletAcceler = attackData.BulletAcceler;
        float bulletMinSpeed = attackData.BulletMinSpeed;
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShotBase = firePoint.up;
            Vector2 directionShot = directionShotBase.RotateDirection(attackData.RangeSpreadAngle.GetRandomValue());
            FrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                newBullet.Shoot(bulletSpeed, directionShot, bulletAcceler, bulletMinSpeed);
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        EndAttack();
    }

    public override void StartAttack() {
        attackData = CurAttackData;
    }

    public override void Updating() {
        bossAttack.B02Base.LookTarget();
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private RangeFloatValue rangeSpreadAngle;
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletAcceler;
        [SerializeField] private float bulletMinSpeed;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public RangeFloatValue RangeSpreadAngle { get => rangeSpreadAngle; }
        public float BulletSpeed { get => bulletSpeed; }
        public float BulletAcceler { get => bulletAcceler; }

        public float BulletMinSpeed { get => bulletMinSpeed; }

    }
}
