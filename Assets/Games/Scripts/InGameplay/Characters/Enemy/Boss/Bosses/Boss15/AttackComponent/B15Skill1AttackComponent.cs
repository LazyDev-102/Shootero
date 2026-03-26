using UnityEngine;
using System;
using System.Collections;
using Gemmob;

public class B15Skill1AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B15Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private Transform leftFirePoint;
    [SerializeField] private Transform rightFirePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private ParticleSystem muzzleLeft;
    [SerializeField] private ParticleSystem muzzleRight;

    [SerializeField] private int numberPreload;

    private Countdowner delayCd = new Countdowner();
    private Countdowner deltaCd = new Countdowner();
    private AttackData attackData;
    private int cNumberBullet;
    private bool canStartAttack;

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
        delayCd.StartCountdown(attackData.DelayAttack);
        canStartAttack = true;
        cNumberBullet = 0;
    }

    public override void Updating() {
        if (!canStartAttack)
            return;
        if (delayCd.IsTimeOut()) {
            if (deltaCd.IsTimeOut()) {
                if (cNumberBullet < attackData.NumberShot) {
                    cNumberBullet++;
                    Shotting(cNumberBullet % 2 == 0);
                    deltaCd.StartCountdown(attackData.DeltaShot);
                }
                else
                    EndAttack();
            }
            deltaCd.Countdowning(Time.deltaTime);
        }
        delayCd.Countdowning(Time.deltaTime);
    }
    public override void Attacking() {
    }

    private void Shotting(bool isLeft) {
        if (muzzleLeft && isLeft) {
            muzzleLeft.Play();
        }
        if (muzzleRight && !isLeft) {
            muzzleRight.Play();
        }
        Vector2 directionShot = isLeft ? bossAttack.Target.position - leftFirePoint.position : bossAttack.Target.position - rightFirePoint.position;
        FrontBullet newBullet = GameLoader.SpawnBullet(bullet, isLeft ? leftFirePoint.position : rightFirePoint.position);
        if (newBullet) {
            newBullet = ChangingBullet(newBullet);
            newBullet.Shoot(attackData.BulletSpeed, directionShot);
        }
    }
    public override void EndAttack() {
        base.EndAttack();
        DespawnAll();
    }
    public override void StopAttack() {
        base.StopAttack();
        DespawnAll();
    }
    private void DespawnAll() {
        canStartAttack = false;
    }


    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaShot;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float delayAttack;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public float BulletSpeed { get => bulletSpeed; }
        public float DelayAttack { get => delayAttack; }
    }
}
