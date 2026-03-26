using UnityEngine;
using System;
using System.Collections;
using Gemmob;

public class HB01Skill3AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private HB01Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
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

    public override void StartAttack() {
        attackData = CurAttackData;
    }

    public override void Updating() {
        bossAttack.HB01Base.LookTarget();
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        float bulletSpeed = attackData.BulletSpeed;
        int damage = (int)(bossAttack.HB01Base.HB01Stat.Atk.Value * attackData.DamagePercent);
        for (int i = 0; i < attackData.NumberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            var originPos = firePoint.position;
            var normalLizeX = transform.right.normalized;
            var normalLizeY = transform.up.normalized;
            Vector2 directionShot = (bossAttack.Target.position - firePoint.position).normalized;
            for (int j = 0; j < attackData.BulletCount / 2; j++) {
                var offsetX = j == 0 ? normalLizeX * (j + 1) * attackData.DistanceBase : normalLizeX * (j + 1) * attackData.Distance;
                var offsetY = normalLizeY * (j * attackData.DistanceY);
                FrontBullet leftBullet = GameLoader.SpawnBullet(bullet, originPos - offsetX - offsetY);
                if (leftBullet) {
                    leftBullet.SetHitInfor(damage, null, bossAttack.HB01Base);
                    leftBullet.Shoot(bulletSpeed, directionShot, attackData.AccelerBullet, attackData.MinSpeedBullet);
                }
                FrontBullet rightBullet = GameLoader.SpawnBullet(bullet, originPos + offsetX - offsetY);
                if (rightBullet) {
                    rightBullet.SetHitInfor(damage, null, bossAttack.HB01Base);
                    rightBullet.Shoot(bulletSpeed, directionShot, attackData.AccelerBullet, attackData.MinSpeedBullet);
                }
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        EndAttack();
    }


    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaShot;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletCount;
        [SerializeField] private float accelerBullet;
        [SerializeField] private float minSpeedBullet;
        [SerializeField] private float distanceBase;
        [SerializeField] private float distance;
        [SerializeField] private float distanceY;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public float BulletSpeed { get => bulletSpeed; }
        public float BulletCount { get => bulletCount; }
        public float AccelerBullet { get => accelerBullet; }
        public float MinSpeedBullet { get => minSpeedBullet; }
        public float DistanceBase { get => distanceBase; }
        public float Distance { get => distance; }
        public float DistanceY { get => distanceY; }
    }
}
