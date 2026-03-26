using System;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B03Skill3AttackComponent : BossSkillBulletAttackComponent {
    [SerializeField] private B03Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private LineRenderer warningLine;
    [SerializeField] private float h;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private float delayAfterAttack;
    [SerializeField] private int numberPreload;

    bool isShoting;
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
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        if (charge) {
            charge.Play();
        }
        DrawWarning();
        yield return Yielder.Wait(delayAttack);
        isShoting = true;
        HideWarning();
        float bulletSpeed = attackData.BulletSpeed;
        Vector2 directionShot = firePoint.up;
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            if (muzzle) {
                muzzle.Play();
            }
            FrontBullet midBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (midBullet) {
                midBullet = ChangingBullet(midBullet);
                midBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                midBullet.Shoot(bulletSpeed, directionShot);
            }
            for (int iBullet = 0; iBullet < attackData.NumberBullet / 2; iBullet++) {
                Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, attackData.SpreadAngle * (iBullet + 1));
                FrontBullet leftBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    leftBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                    leftBullet.Shoot(bulletSpeed, directionLeft);
                }


                Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * attackData.SpreadAngle * (iBullet + 1));
                FrontBullet rightBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    rightBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                    rightBullet.Shoot(bulletSpeed, directionRight);
                }
            }

            yield return Yielder.Wait(attackData.DeltaShot);
        }
        isShoting = false;
        yield return Yielder.Wait(delayAfterAttack);
        EndAttack();
    }

    public override void StopAttack() {
        HideWarning();
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private void DrawWarning() {
        warningLine.SetPosition(0, Vector3.zero);
        warningLine.SetPosition(1, new Vector3(0, h, 1));
        float a = Mathf.Tan(attackData.SpreadAngle * (attackData.NumberBullet / 2) * Mathf.Deg2Rad) * 2 * h;
        warningLine.startWidth = 0;
        warningLine.endWidth = a;
        warningLine.gameObject.SetActive(true);
    }

    private void HideWarning() {
        warningLine.gameObject.SetActive(false);
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        isShoting = false;
    }

    public override void Updating() {
        if (!isShoting) {
            bossAttack.B03Base.LookTarget();
        }
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;
        [SerializeField] private float spreadAngle;
        [SerializeField] private float damagePercent;
        [SerializeField] private int numberBullet;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletAcceler;

        public int NumberShot { get => numberShot; }
        public float DamagePercent { get => damagePercent; }
        public float DeltaShot { get => deltaShot; }
        public int NumberBullet { get => numberBullet; }
        public float BulletSpeed { get => bulletSpeed; }
        public float BulletAcceler { get => bulletAcceler; }
        public float SpreadAngle { get => spreadAngle; }
    }
}
