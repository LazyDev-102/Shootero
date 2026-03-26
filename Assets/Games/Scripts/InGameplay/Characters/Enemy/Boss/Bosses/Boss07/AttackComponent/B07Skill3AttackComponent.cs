using Gemmob;
using System;
using System.Collections;
using UnityEngine;

public class B07Skill3AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B07Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePointLeft;
    [SerializeField] private Transform firePointRight;
    [SerializeField] private HomingBullet bullet;
    [SerializeField] private ParticleSystem appearEffect;
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
    public override void StartAttack() {
        attackData = CurAttackData;
        bossAttack.B07Base.B07Move.EndMoveIdle();
    }
    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        yield return Yielder.Wait(delayAttack);
        Transform target = bossAttack.Target;
        bool isLeft = false;
        for (int ishot = 0; ishot < attackData.NumberShot; ishot++) {
            Vector2 direction = Vector2.one;
            Vector2 firePosition = Vector2.one;
            if (isLeft) {
                direction = firePointLeft.up;
                firePosition = firePointLeft.position;
            }
            else {
                direction = firePointRight.up;
                firePosition = firePointRight.position;

            }
            yield return Yielder.Wait(attackData.TimeDelayPerAttack);
            var effect = appearEffect.Spawn();
            if (effect) {
                effect.transform.position = firePosition;
                effect.Stop();
                effect.Play();
            }
            HomingBullet newBullet = GameLoader.SpawnBullet(bullet, firePosition);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.SetDelayHoming(attackData.DelayHoming);
                newBullet.SetLifeTimeHoming(attackData.TimeLife);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                newBullet.Shoot(attackData.BulletSpeed, target, direction, attackData.BulletAcceler);
            }
            isLeft = !isLeft;
            yield return Yielder.Wait(attackData.DeltaShot);
            effect?.Recycle();
        }
        yield return Yielder.Wait(0.5f);
        EndAttack();
    }



    public override void Updating() {
        bossAttack.B07Base.LookTarget();
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;
        [SerializeField] private float damagePercent;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletAcceler;
        [SerializeField] private float delayHoming;
        [SerializeField] private float timeLife;
        [SerializeField] private float timeDelayPerAttack;



        public float DamagePercent { get => damagePercent; }
        public int NumberShot { get => numberShot; }
        public float DelayHoming { get => delayHoming; }

        public float BulletSpeed { get => bulletSpeed; }
        public float BulletAcceler { get => bulletAcceler; }
        public float TimeLife { get => timeLife; }
        public float DeltaShot { get => deltaShot; }
        public float TimeDelayPerAttack { get => timeDelayPerAttack; }
    }
}
