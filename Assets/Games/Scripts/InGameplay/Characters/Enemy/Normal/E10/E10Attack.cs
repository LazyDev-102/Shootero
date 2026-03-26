

using UnityEngine;
using Gemmob;

public class E10Attack : EnemyAttack {
    private E10Base e10Base;
    public E10Base E10Base {
        get {
            if (e10Base == null) {
                e10Base = EnemyBase as E10Base;
            }
            return e10Base;
        }
    }

    [SerializeField] private float aimTime;
    [SerializeField] private float deltaAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private BoomerangBullet bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float delayAfterShot;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;


    GameLoader gameLoader;
    private Countdowner aimCountdowner = new Countdowner();
    private Countdowner afterShotCountdowner = new Countdowner();

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }


    public override void Initialize() {
        base.Initialize();
        gameLoader = GameManager.Instance.GameLoader;
    }

    public void StartAimTarget() {
        aimCountdowner.StartCountdown(aimTime);
        isAttacking = false;
    }

    public void AimTarget() {
        E10Base.LookTarget();
        aimCountdowner.Countdowning(Time.deltaTime);
    }

    public override bool CanAttack() {
        return aimCountdowner.IsTimeOut() && !isAttacking;
    }

    protected override void Attacking() {
        if (muzzle) {
            muzzle.Play();
        }
        BoomerangBullet newBullet = gameLoader.SpawnBullet(bullet, firePoint.position);
        if (newBullet) {
            newBullet = ChangingBullet(newBullet);
            newBullet.SetInfo(deltaAttack);
            newBullet.SetTransformTarget(transform);
            newBullet.AddOnEndBack(OnBoomerangBack);
            newBullet.SetSize(E10Base.E10Stat.Size.Value);
            newBullet.Shoot(Target.position, bulletSpeed);
            E10Base.RemoveAllOnDie();
            E10Base.AddOnDie(() => {
                newBullet.SelfDestruction();
            });
        }
        afterShotCountdowner.StartCountdown(delayAfterShot);
    }

    private void OnBoomerangBack() {
        EndAttack();
    }

    public override void EndAttack() {
        aimCountdowner.StartCountdown(aimTime);
        base.EndAttack();
    }

    public void EndShotCountdown() {
        afterShotCountdowner.Countdowning(Time.deltaTime);
    }

    public bool IsEndShot() {
        return afterShotCountdowner.IsTimeOut();
    }

}
