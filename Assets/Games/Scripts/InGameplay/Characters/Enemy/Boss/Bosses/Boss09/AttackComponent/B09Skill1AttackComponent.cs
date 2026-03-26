using UnityEngine;
using System;
using System.Collections;
using Gemmob;
using System.Collections.Generic;

public class B09Skill1AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B09Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField, Range(0, 10)] private float minSpeed = 2;
    [SerializeField, Range(0, 10)] private float waitTimeBlur = 5;
    [SerializeField, Range(-10, 10)] private float acceleration = -1;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private int numberPreload;

    private List<FrontBullet> bullets;

    private Countdowner blurCoundowner = new Countdowner();
    private AttackData CurAttackData {
        get {
            return attackDatas[CurrentPhaseIndex];
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
    public override void Initialize() {
        base.Initialize();
        bullets = new List<FrontBullet>();
    }
    public override void StartAttack() {
        bullets?.Clear();
    }

    public override void Updating() {
        bossAttack.B09Base.LookTarget();
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        float bulletSpeed = CurAttackData.BulletSpeed;
        yield return Yielder.Wait(delayAttack);
        for (int i = 0; i < CurAttackData.NumberShot; ++i) {
            Vector2 directionShot = bossAttack.Target.position - firePoint.position;
            FrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(CurAttackData.DamagePercent - 1, StatModType.PercentMult));
                newBullet.Shoot(bulletSpeed, directionShot, acceleration, minSpeed);
                newBullet.SetAlpha(1);
                bullets.Add(newBullet);
                if (gameObject.activeInHierarchy)
                    StartCoroutine(BlurBullet(newBullet));
            }
            yield return Yielder.Wait(CurAttackData.DeltaShot);
        }
        yield return Yielder.Wait(3);
        EndAttack();
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
    public override void EndAttack() {
        if (bullets == null) {
            base.EndAttack();
        }
        else {
            foreach (var item in bullets) {
                if (item != null)
                    item.Recycle();
            }
            bullets.Clear();
            base.EndAttack();
        }
    }
    private IEnumerator BlurBullet(FrontBullet bullet) {
        yield return Yielder.Wait(waitTimeBlur);
        blurCoundowner.StartCountdown(1);
        while (blurCoundowner.Countdown > 0) {
            bullet.SetAlpha(blurCoundowner.Countdown);
            blurCoundowner.Countdowning(Time.deltaTime);
            yield return null;
        }
        bullet.SetAlpha(0);
        GameLoader.RemoveBullet(bullet);
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaShot;
        [SerializeField] private float bulletSpeed;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public float BulletSpeed { get => bulletSpeed; }

    }
}