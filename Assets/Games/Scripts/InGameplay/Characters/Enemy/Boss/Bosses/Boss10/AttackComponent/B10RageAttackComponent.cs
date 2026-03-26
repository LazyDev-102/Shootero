using Gemmob;
using Helper;
using System;
using System.Collections;
using UnityEngine;

public class B10RageAttackComponent : BossAttackComponent {
    [SerializeField] private B10Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private float delayAfterAttack;
    [SerializeField] private int numberPreload;

    [Header("Lightning")]
    [SerializeField] private B10LightningLine leftLine;
    [SerializeField] private B10LightningLine rightLine;
    private bool canAim;
    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B10Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B10Base.CurrentPhaseIndex];
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
        leftLine.gameObject.SetActive(true);
        rightLine.gameObject.SetActive(true);
        attackData = CurAttackData;
        canAim = true;
        leftLine.transform.SetParent(null);
        leftLine.transform.eulerAngles = new Vector3();
        rightLine.transform.SetParent(null);
        rightLine.transform.eulerAngles = new Vector3();

    }

    public override void Updating() {
        if (canAim)
            bossAttack.B10Base.LookTarget();

    }

    public override void Attacking() {
        StartLighiLine();

        if (gameObject.activeInHierarchy)
            StartCoroutine(Shot());
    }


    private IEnumerator Shot() {
        if (charge) {
            charge.Play();
        }
        yield return Yielder.Wait(delayAttack);
        float bulletSpeed = attackData.BulletSpeed;
        float bulletAcceler = attackData.BulletAcceler;
        float bulletMinSpeed = attackData.BulletMinSpeed;
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShotBase = firePoint.up;
            Vector2 directionShot = directionShotBase.RotateDirection(attackData.RangeSpreadAngle.GetRandomValue());
            FrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamageBulletPercent - 1, StatModType.PercentMult));
                newBullet.Shoot(bulletSpeed, directionShot, bulletAcceler, bulletMinSpeed);
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        leftLine.StartMoveIn();
        rightLine.StartMoveIn();
        canAim = false;
        yield return Yielder.Wait(delayAfterAttack);
        EndAttack();
    }

    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(GetBossAttack().BossBase.BossStat.Atk.Value, null, GetBossAttack().BossBase);
        return bullet;
    }

    private void StartLighiLine() {
        //canAim = false;
        leftLine.transform.position = BorderHelper.GetWorldPointInsideArea(new Vector2(0.1f, 0.5f));
        leftLine.Show();
        leftLine.StartMoveOut();

        rightLine.transform.position = BorderHelper.GetWorldPointInsideArea(new Vector2(0.9f, 0.5f));
        rightLine.Show();
        rightLine.StartMoveOut();
    }

    public override void EndAttack() {
        base.EndAttack();
        leftLine.transform.SetParent(transform);
        rightLine.transform.SetParent(transform);
        leftLine.Hide();
        rightLine.Hide();
    }
    public override void StopAttack() {
        base.StopAttack();
        leftLine.Hide();
        rightLine.Hide();
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private RangeFloatValue rangeSpreadAngle;
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;
        [SerializeField] private float damageBulletPercent;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletAcceler;
        [SerializeField] private float bulletMinSpeed;
        [Header("Lightning")]
        [SerializeField] private float damageLightningPercent;


        public int NumberShot { get => numberShot; }
        public float DamageBulletPercent { get => damageBulletPercent; }
        public float DeltaShot { get => deltaShot; }
        public RangeFloatValue RangeSpreadAngle { get => rangeSpreadAngle; }
        public float BulletSpeed { get => bulletSpeed; }
        public float BulletAcceler { get => bulletAcceler; }
        public float BulletMinSpeed { get => bulletMinSpeed; }

        public float DamageLightningPercent { get => damageLightningPercent; }


    }
}
