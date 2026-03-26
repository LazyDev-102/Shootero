

using System.Collections;
using UnityEngine;
using Gemmob;

public class E06Attack : EnemyAttack {
    private E06Base e06Base;
    public E06Base E06Base {
        get {
            if (e06Base == null) {
                e06Base = EnemyBase as E06Base;
            }
            return e06Base;
        }
    }

    [SerializeField] private float delayAttack;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private HomingBullet bullet;
    [SerializeField] private float speedBullet;
    [SerializeField] private float size;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;

    GameLoader gameLoader;
    private bool firstAttack;
    private Countdowner aimCountdowner = new Countdowner();

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    public override void Initialize() {
        base.Initialize();
        firstAttack = true;
        gameLoader = GameManager.Instance.GameLoader;
    }

    public override bool CanAttack() {
        return !isAttacking && aimCountdowner.IsTimeOut();
    }

    public void StartAimTarget() {
        aimCountdowner.StartCountdown(firstAttack ? 0.5f : 1f / E06Base.E06Stat.AtkSpeed.Value);
        if (firstAttack) {
            firstAttack = false;
        }
    }
    protected override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Shoting());
    }

    private IEnumerator Shoting() {
        yield return Yielder.Wait(delayAttack + aimCountdowner.Countdown);
        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = Target.position - transform.position;
            HomingBullet centerBullet = gameLoader.SpawnBullet(bullet, transform.position);
            if (centerBullet) {
                centerBullet = ChangingBullet(centerBullet);
                centerBullet.Shoot(speedBullet, Target, directionShot);
                centerBullet.SetSize(size * E06Base.E06Stat.Size.Value);
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }


    public void AimTarget() {
        E06Base.LookTarget();
    }
}
