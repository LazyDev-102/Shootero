using Gemmob;
using Helper;
using System.Collections;
using UnityEngine;

public class B02RageAttackComponent : BossAttackComponent {
    [SerializeField] private B02Attack bossAttack;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private float fireRate;
    [SerializeField] private B02MiniGunComponent[] miniguns;
    [SerializeField] private int numberPreload;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;

    private Countdowner lifeCountdowner = new Countdowner();
    private Countdowner fireRateCountdowner = new Countdowner();

    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B02Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B02Base.CurrentPhaseIndex];
        }
    }


    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }


    public override void Attacking() {
        StartMiniAttack();
    }

    public override void Initialize() {
        foreach (var mini in miniguns) {
            mini.Initialize();
        }
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        foreach (var mini in miniguns) {
            mini.gameObject.SetActive(false);
        }
        bossAttack.BossBase.BossMove.EndMoveIdle();
    }

    public override void Updating() {
        if (lifeCountdowner.IsCountdowning()) {
            float deltaTime = Time.deltaTime;
            fireRateCountdowner.Countdowning(deltaTime);
            if (fireRateCountdowner.IsTimeOut()) {
                fireRateCountdowner.StartCountdown(fireRate);
                if (gameObject.activeInHierarchy)
                    StartCoroutine(IShot());
            }
            lifeCountdowner.Countdowning(deltaTime);
            foreach (var mini in miniguns) {
                mini.LookTarget(bossAttack.Target);
            }

            if (lifeCountdowner.IsTimeOut()) {
                StopAllCoroutines();
                DeActiveGun();
            }
        }
    }

    public override void StopAttack() {
        base.StopAttack();
        foreach (var mini in miniguns) {
            mini.gameObject.SetActive(false);
        }
    }

    private void StartMiniAttack() {
        // spawn MiniGun
        ActiveMiniGun();
        lifeCountdowner.StartCountdown(attackData.LifeDuration);
        fireRateCountdowner.StartCountdown(fireRate);
    }

    private void ActiveMiniGun() {
        foreach (var mini in miniguns) {
            mini.gameObject.SetActive(true);
            mini.Show();
        }
    }

    private void DeActiveGun() {
        foreach (var mini in miniguns) {
            mini.Hide(() => {
                mini.gameObject.SetActive(false);
            });
        }
        this.DelayWait(1f, EndAttack);
    }

    private IEnumerator IShot() {
        Vector3 positionSpawn = new Vector3(100, 100, 0);
        for (int ishot = 0; ishot < attackData.NumberShot; ++ishot) {
            foreach (var mini in miniguns) {
                FrontBullet newBullet = GameLoader.SpawnBullet(bullet, positionSpawn);
                if (newBullet) {
                    newBullet = ChangingBullet(newBullet);
                    newBullet.HitInfor.Damage.AddModifier(new StatModifier(attackData.DamagePercent - 1, StatModType.PercentMult));
                    mini.Shot(newBullet, attackData.BulletSpeed);
                }
            }
            yield return Yielder.Wait(attackData.DeltaShot);
        }
    }

    private T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(GetBossAttack().BossBase.BossStat.Atk.Value, null, GetBossAttack().BossBase);
        //foreach(var mod in ) {
        //  mod.ChangeBullet(bulletChanged);
        //}

        return bullet;
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    [System.Serializable]
    private class AttackData {
        [SerializeField] private float lifeDuration;
        [SerializeField] private int numberShot;
        [SerializeField] private float deltaShot;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float damagePercent;

        public float LifeDuration { get => lifeDuration; }
        public int NumberShot { get => numberShot; }
        public float DeltaShot { get => deltaShot; }
        public float BulletSpeed { get => bulletSpeed; }
        public float DamagePercent { get => damagePercent; }
    }
}
