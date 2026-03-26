using UnityEngine;
using Helper;
using System;
using Gemmob;

public class HB01Skill1AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private HB01Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private Area point1;
    [SerializeField] private Area point2;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RotateFrontTargetBullet bullet;
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
            Shotting();
    }

    private void Shotting() {
        if (charge) {
            charge.Play();
        }
        for (int i = 0; i < 2; ++i) {
            if (muzzle) {
                muzzle.Play();
            }

            Vector2 pos = BorderHelper.GetWorldPointInsideArea(i == 0 ? point1 : point2);
            RotateFrontTargetBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.SetSize(attackData.BulletSize);
                newBullet.Shoot(pos, attackData.Move2TargetTime, attackData.DeltaAttack, attackData.RotateTimeSpeed, attackData.TimeLife);
            }

        }
        EndAttack();
    }

    public override void StartAttack() {
        attackData = CurAttackData;
    }

    public override void Updating() {
        bossAttack.HB01Base.LookTarget();
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
    public RotateFrontTargetBullet ChangingBullet(RotateFrontTargetBullet bullet) {
        bullet.SetHitInfor((int)(bossAttack.HB01Base.HB01Stat.Atk.Value * attackData.DamagePercent), null, bossAttack.BossBase);

        return bullet;
    }
    [Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaAttack;
        [SerializeField] private float move2TargetTime;
        [SerializeField] private float bulletSize;
        [SerializeField] private float rotateTimeSpeed;
        [SerializeField] private float timeLife;



        public float DamagePercent { get => damagePercent; }
        public float DeltaAttack { get => deltaAttack; }
        public float Move2TargetTime { get => move2TargetTime; }
        public float BulletSize { get => bulletSize; }
        public float RotateTimeSpeed { get => rotateTimeSpeed; }
        public float TimeLife { get => timeLife; }


    }
}
