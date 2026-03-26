using Helper;
using System;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B07RageAttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B07Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private int numberPreload;

    private AttackData attackData;
    bool isToLeft;
    bool enableRotate;
    Vector2 directionRotate;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B07Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B07Base.CurrentPhaseIndex];
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
        enableRotate = false;
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        yield return Yielder.Wait(delayAttack);
        enableRotate = true;
        isToLeft = RandomHelper.RandomWithProbability(50);
        for (int iturn = 0; iturn < attackData.NumberTurn; ++iturn) {
            for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
                for (int ipoint = 0; ipoint < firePoints.Length; ++ipoint) {
                    Vector2 direction = firePoints[ipoint].up;
                    FrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoints[ipoint].position);
                    if (newBullet) {
                        newBullet = ChangingBullet(newBullet);
                        newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                        newBullet.Shoot(attackData.BulletSpeed, direction, attackData.BulletAcceler);
                    }
                }
                if (ishot != attackData.NumberShot - 1) {
                    yield return Yielder.Wait(attackData.DeltaShot);
                }
            }
            isToLeft = !isToLeft;
            yield return Yielder.Wait(attackData.DeltaTurn);
        }
        enableRotate = false;
        EndAttack();
    }



    public override void Updating() {
        if (enableRotate) {
            if (isToLeft) {
                directionRotate = transform.up.RotateDirection(10);
            }
            else {
                directionRotate = transform.up.RotateDirection(-10);
            }
            bossAttack.B07Base.B07Move.LookDirection(directionRotate);
        }
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private int numberTurn;
        [SerializeField] private float deltaTurn;
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private int numberBullet;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletAcceler;
        [SerializeField] private float deltaAngle;

        public int NumberTurn { get => numberTurn; }
        public float DeltaTurn { get => deltaTurn; }
        public int NumberShot { get => numberShot; }
        public float DeltaShot { get => deltaShot; }
        public float DamagePercent { get => damagePercent; }
        public int NumberBullet { get => numberBullet; }
        public float BulletSpeed { get => bulletSpeed; }
        public float BulletAcceler { get => bulletAcceler; }
        public float DeltaAngle { get => deltaAngle; }
    }
}
