using UnityEngine;
using System;
using System.Collections;
using Gemmob;

public class B01Skill1AttackComponent : BossSkillBulletAttackComponent {
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

    public override void StartAttack() {
        attackData = CurAttackData;
    }

    public override void Updating() {
        bossAttack.B01Base.LookTarget();
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        float bulletSpeed = attackData.BulletSpeed;
        if (charge) {
            charge.Play();
        }
        yield return Yielder.Wait(delayAttack);
        for (int i = 0; i < attackData.NumberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = bossAttack.Target.position - firePoint.position;
            FrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                newBullet.Shoot(bulletSpeed, directionShot, attackData.AccelerBullet, attackData.MinSpeedBullet);
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        EndAttack();
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }



    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaShot;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float accelerBullet;
        [SerializeField] private float minSpeedBullet;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public float BulletSpeed { get => bulletSpeed; }
        public float AccelerBullet { get => accelerBullet; }
        public float MinSpeedBullet { get => minSpeedBullet; }


    }
}
