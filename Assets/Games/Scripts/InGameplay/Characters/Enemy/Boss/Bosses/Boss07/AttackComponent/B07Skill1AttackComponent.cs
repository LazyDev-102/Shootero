using System;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B07Skill1AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B07Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private SinBullet bullet;
    [SerializeField] private RangeFloatValue amplitudeRange;
    [SerializeField] private RangeFloatValue cycleRange;
    [SerializeField] private float distance;
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
        bossAttack.B07Base.LookTarget();
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        float bulletSpeed = attackData.BulletSpeed;
        yield return Yielder.Wait(delayAttack);
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            Vector2 direction = firePoint.up;
            Vector2 positionMid = firePoint.position;
            SinBullet newBullet = GameLoader.SpawnBullet(bullet, positionMid);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                newBullet.Shoot(bulletSpeed, direction, amplitudeRange.GetRandomValue(), cycleRange.GetRandomValue());
            }
            for (int ibullet = 0; ibullet < attackData.NumberBullet / 2; ++ibullet) {

                Vector2 positionRight = firePoint.position + (ibullet + 1) * distance * firePoint.right;
                SinBullet newBulletRight = GameLoader.SpawnBullet(bullet, positionRight);
                if (newBulletRight) {
                    newBulletRight = ChangingBullet(newBulletRight);
                    newBulletRight.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                    newBulletRight.Shoot(bulletSpeed, direction, amplitudeRange.GetRandomValue(), cycleRange.GetRandomValue());
                }

                Vector2 positionLeft = firePoint.position + (ibullet + 1) * distance * firePoint.right * -1;
                SinBullet newBulletLeft = GameLoader.SpawnBullet(bullet, positionLeft);
                if (newBulletLeft) {
                    newBulletLeft = ChangingBullet(newBulletLeft);
                    newBulletLeft.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                    newBulletLeft.Shoot(bulletSpeed, direction, amplitudeRange.GetRandomValue(), cycleRange.GetRandomValue());
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
        [SerializeField] private float numberBullet;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public float BulletSpeed { get => bulletSpeed; }
        public float NumberBullet { get => numberBullet; }
    }
}
