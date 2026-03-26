using UnityEngine;
using System;
using Helper;
using System.Collections.Generic;
using Gemmob;

public class B06Skill1AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B06Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RotateFrontBullet bullet;
    [SerializeField] private int numberPreload;

    private Countdowner duration = new Countdowner();
    private Countdowner deltaShot = new Countdowner();
    private Countdowner delayAttackCoundowner = new Countdowner();
    private bool hasAttack;
    private List<RotateFrontBullet> bullets = new List<RotateFrontBullet>();
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

    public override void Attacking() {
        duration.StartCountdown(attackData.Duration);
        deltaShot.StartCountdown(0);
        delayAttackCoundowner.StartCountdown(delayAttack);
    }

    private void Shotting() {
        if (hasAttack)
            return;
        delayAttackCoundowner.Countdowning(Time.deltaTime);
        if (delayAttackCoundowner.IsTimeOut()) {
            float bulletSpeed = attackData.BulletSpeed;
            Vector2 directionShotBase = firePoint.up;
            duration.Countdowning(Time.deltaTime);
            if (duration.IsCountdowning()) {
                deltaShot.Countdowning(Time.deltaTime);
                if (deltaShot.IsTimeOut()) {
                    Vector2 directionShot = directionShotBase.RotateDirection(attackData.RangeSpreadAngle.GetRandomValue());
                    RotateFrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                    if (newBullet) {
                        newBullet = ChangingBullet(newBullet);
                        newBullet.SetInfo(0.1f);
                        newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                        newBullet.Shoot(directionShot, bulletSpeed, attackData.Acceleration);
                        bullets.Add(newBullet);
                    }
                    deltaShot.StartCountdown(attackData.DeltaShot);
                }
            }
            else {
                hasAttack = true;
                EndAttack();
            }
        }
    }


    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        bullets?.Clear();
        hasAttack = false;
    }

    public override void Updating() {
        bossAttack.B06Base.LookTarget();
        Shotting();
    }
    public override void EndAttack() {
        bullets?.Clear();
        base.EndAttack();
    }
    public override void StopAttack() {
        if (bullets == null) {
            base.StopAttack();
        }
        else {
            foreach (var item in bullets) {
                if (item != null)
                    item.Recycle();
            }
            bullets.Clear();
            base.StopAttack();
        }
    }
    [Serializable]
    private class AttackData {
        [SerializeField] private RangeFloatValue rangeSpreadAngle;
        [SerializeField] private float deltaShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float duration;
        [SerializeField] private float acceleration;

        public RangeFloatValue RangeSpreadAngle { get => rangeSpreadAngle; }
        public float DeltaShot { get => deltaShot; }
        public float DamagePercent { get => damagePercent; }
        public float BulletSpeed { get => bulletSpeed; }
        public float Duration { get => duration; }
        public float Acceleration { get => acceleration; }
    }
}