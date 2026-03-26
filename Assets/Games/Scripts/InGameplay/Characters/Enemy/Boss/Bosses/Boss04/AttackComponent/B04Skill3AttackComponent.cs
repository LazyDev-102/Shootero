

using System;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B04Skill3AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B04Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private HomingBullet bullet;
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
        Transform target = bossAttack.Target;
        float bulletSpeed = attackData.BulletSpeed;
        float bulletAcceler = attackData.BulletAcceler;
        Vector2 directionShot = firePoint.up;
        for (int iBullet = 0; iBullet < attackData.NumberBullet / 2; iBullet++) {
            Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, attackData.SpreadAngle * (iBullet + 1));
            HomingBullet leftBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (leftBullet) {
                leftBullet = ChangingBullet(leftBullet);
                leftBullet.SetDelayHoming(attackData.DelayHoming);
                leftBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                leftBullet.Shoot(bulletSpeed, target, directionLeft, bulletAcceler);
            }
            Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * attackData.SpreadAngle * (iBullet + 1));
            HomingBullet rightBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (rightBullet) {
                rightBullet = ChangingBullet(rightBullet);
                rightBullet.SetDelayHoming(attackData.DelayHoming);
                rightBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                rightBullet.Shoot(bulletSpeed, target, directionRight, bulletAcceler);
            }
        }
        yield return Yielder.Wait(0.5f);
        EndAttack();
    }

    public override void StartAttack() {
        attackData = CurAttackData;
    }

    public override void Updating() {
        bossAttack.B04Base.LookTarget();
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private int numberBullet;
        [SerializeField] private float spreadAngle;
        [SerializeField] private float damagePercent;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletAcceler;
        [SerializeField] private float delayHoming;


        public float DamagePercent { get => damagePercent; }
        public int NumberBullet { get => numberBullet; }
        public float SpreadAngle { get => spreadAngle; }
        public float DelayHoming { get => delayHoming; }

        public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
        public float BulletAcceler { get => bulletAcceler; set => bulletAcceler = value; }

    }
}
