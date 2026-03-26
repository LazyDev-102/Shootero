using System;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B03Skill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B03Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineLightningBullet bullet;
    [SerializeField] private float bulletSpeed;
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
        bossAttack.B03Base.LookTarget();
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
        int damage = bossAttack.BossBase.BossStat.Atk.Value;
        int damageCircle = (int)(damage * attackData.DamageCirclePercent);
        int damageLine = (int)(damage * attackData.DamageLinePercent);

        for (int i = 0; i < attackData.NumberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = firePoint.up;
            LineLightningBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet.SetInfor(damageCircle, damageLine, bossAttack.B03Base);
                newBullet.Shoot(bulletSpeed, directionShot);
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
        [SerializeField] private float damageCirclePercent;
        [SerializeField] private float damageLinePercent;
        [SerializeField] private float deltaShot;

        public int NumberShot { get => numberShot; }
        public float DamageCirclePercent { get => damageCirclePercent; }
        public float DamageLinePercent { get => damageLinePercent; }
        public float DeltaShot { get => deltaShot; }
    }
}
