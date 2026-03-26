

using System;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B04Skill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B04Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AutoExplosionBullet bullet;
    [SerializeField] private Explosioner explosioner;
    [SerializeField] private int numberPreload;

    bool isShoting;

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
        isShoting = true;
        float bulletSpeed = attackData.BulletSpeed;
        float bulletAcceler = attackData.BulletAcceler;
        float lifeTime = attackData.TimeLife;
        Vector2 directionShot = firePoint.up;
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            AutoExplosionBullet midBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (midBullet) {
                midBullet = ChangingBullet(midBullet);
                midBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamageBulletPercent - 1, StatModType.PercentMult));
                midBullet.AddOnDestroy(OnBulletExplosion);
                midBullet.Shoot(bulletSpeed, directionShot, bulletAcceler, timeLife: lifeTime);
                midBullet.SetAlpha(1);
            }
            for (int iBullet = 0; iBullet < attackData.NumberBullet / 2; iBullet++) {
                Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, attackData.SpreadAngle * (iBullet + 1));
                AutoExplosionBullet leftBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    leftBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamageBulletPercent - 1, StatModType.PercentMult));
                    leftBullet.AddOnDestroy(OnBulletExplosion);
                    leftBullet.Shoot(bulletSpeed, directionLeft, bulletAcceler, timeLife: lifeTime);
                    leftBullet.SetAlpha(1);
                }

                Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * attackData.SpreadAngle * (iBullet + 1));
                AutoExplosionBullet rightBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    rightBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamageBulletPercent - 1, StatModType.PercentMult));
                    rightBullet.AddOnDestroy(OnBulletExplosion);
                    rightBullet.Shoot(bulletSpeed, directionRight, bulletAcceler, timeLife: lifeTime);
                    rightBullet.SetAlpha(1);
                }
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        isShoting = false;
        EndAttack();
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        isShoting = false;
    }

    public override void Updating() {
        if (!isShoting) {
            bossAttack.B04Base.LookTarget();
        }
    }

    private void OnBulletExplosion(Vector3 position) {
        Explosioner newExplosioner = GameManager.Instance.GameLoader.SpawnExplosion(explosioner, position);
        if (newExplosioner) {
            newExplosioner.SetHitInfor((int)(bossAttack.B04Base.B04Stat.Atk.Value * attackData.DamageExplosionPercent), null, bossAttack.B04Base)
                            .SetRadius(attackData.Radius)
                            .Explosioning();
        }
    }


    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;
        [SerializeField] private float spreadAngle;
        [SerializeField] private float damageBulletPercent;
        [SerializeField] private int numberBullet;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletAcceler;
        [SerializeField] private float timeLife;
        [SerializeField] private float radius;
        [SerializeField] private float damageExplosionPercent;

        public int NumberShot { get => numberShot; }
        public float DamageBulletPercent { get => damageBulletPercent; }
        public float DeltaShot { get => deltaShot; }
        public int NumberBullet { get => numberBullet; }
        public float BulletSpeed { get => bulletSpeed; }
        public float BulletAcceler { get => bulletAcceler; }
        public float SpreadAngle { get => spreadAngle; }
        public float DamageExplosionPercent { get => damageExplosionPercent; }
        public float TimeLife { get => timeLife; }
        public float Radius { get => radius; }



    }
}
