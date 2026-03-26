using UnityEngine;
using System.Collections;
using Helper;
using System;
using Gemmob;

public class B01Skill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B01Attack bossAttack;
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
        yield return Yielder.Wait(delayAttack);
        float bulletSpeed = attackData.BulletSpeed;
        if (charge) {
            charge.Play();
        }
        Vector2 directionShot = firePoint.up;
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            if (muzzle) {
                muzzle.Play();
            }
            FrontBullet midBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (midBullet) {
                midBullet = ChangingBullet(midBullet);
                midBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                midBullet.Shoot(bulletSpeed, directionShot);
            }
            for (int iBullet = 0; iBullet < attackData.NumberBullet / 2; iBullet++) {
                Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, attackData.SpreadAngle * (iBullet + 1));
                FrontBullet leftBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    leftBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                    leftBullet.Shoot(bulletSpeed, directionLeft);
                }

                Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * attackData.SpreadAngle * (iBullet + 1));
                FrontBullet rightBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    rightBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                    rightBullet.Shoot(bulletSpeed, directionRight);
                }
            }

            yield return Yielder.Wait(attackData.DeltaShot);
        }
        EndAttack();
    }

    public override void StartAttack() {
        attackData = CurAttackData;
    }

    public override void Updating() {
        bossAttack.B01Base.LookTarget();
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private int numberBullet;
        [SerializeField] private float spreadAngle;
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float bulletSpeed;


        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public int NumberBullet { get => numberBullet; }
        public float SpreadAngle { get => spreadAngle; }
        public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    }
}
