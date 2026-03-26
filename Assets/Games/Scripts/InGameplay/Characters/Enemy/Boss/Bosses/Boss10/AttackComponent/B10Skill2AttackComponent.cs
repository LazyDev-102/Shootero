

using Helper;
using System;
using System.Collections;
using UnityEngine;
using Gemmob;
using System.Collections.Generic;

public class B10Skill2AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B10Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private B10LightningBallBullet bullet;
    [SerializeField] private Area zoneArea;
    [SerializeField] private float minDistance;
    [SerializeField] private float deltaAttack;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;

    private List<Vector2> points;
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
        points = new List<Vector2>();
    }

    public override void Updating() {
        bossAttack.B10Base.LookTarget();
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
        float bulletSpeed = attackData.BulletSpeed;
        float bulletAcceler = attackData.BulletAcceler;
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            if (muzzle) {
                muzzle.Play();
            }

            int count = 0;
            Vector2 targetPoint = Vector2.zero;
            bool fail = false;
            do {
                fail = false;
                targetPoint = BorderHelper.GetWorldPointInsideArea(zoneArea);
                count++;
                foreach (var p in points) {
                    if (Vector2.Distance(p, targetPoint) < minDistance) {
                        fail = true;
                        break;
                    }
                }
            }
            while (fail && count < 30);
            points.Add(targetPoint);
            B10LightningBallBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                newBullet.SetInfo(deltaAttack, attackData.LifeTime);
                newBullet.Shoot(targetPoint, bulletSpeed, bulletAcceler);
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
        [SerializeField] private float bulletAcceler;
        [SerializeField] private float lifeTime;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public float BulletSpeed { get => bulletSpeed; }
        public float BulletAcceler { get => bulletAcceler; }
        public float LifeTime { get => lifeTime; }
    }
}
