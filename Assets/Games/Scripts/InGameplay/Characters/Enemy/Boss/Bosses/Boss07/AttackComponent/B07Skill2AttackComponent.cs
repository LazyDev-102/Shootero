

using Helper;
using System;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B07Skill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B07Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private RangeIntValue randomAngle;
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

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        yield return Yielder.Wait(delayAttack);
        float bulletSpeed = attackData.BulletSpeed;
        float bulletAcceler = attackData.BulletAcceler;
        int numberBullet = attackData.NumberBullet;

        float deltaAngle = 360.0f / numberBullet;
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            for (int ipoint = 0; ipoint < firePoints.Length; ++ipoint) {
                Vector2 directionShotBase = firePoints[ipoint].up.RotateDirection(randomAngle.GetRandomValue());
                for (int ibullet = 0; ibullet < attackData.NumberBullet; ++ibullet) {
                    Vector2 directionShot = directionShotBase.RotateDirection(deltaAngle * ibullet);
                    FrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoints[ipoint].position);
                    if (newBullet) {
                        newBullet = ChangingBullet(newBullet);
                        newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                        newBullet.Shoot(bulletSpeed, directionShot, bulletAcceler);
                    }
                }
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        EndAttack();
    }



    public override void Updating() {
        bossAttack.B07Base.B07Move.LookDirection(UnityHelper.Down);
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private int numberBullet;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletAcceler;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public int NumberBullet { get => numberBullet; }
        public float BulletSpeed { get => bulletSpeed; }
        public float BulletAcceler { get => bulletAcceler; }
    }
}
