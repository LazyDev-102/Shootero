
using Gemmob;
using System.Collections;
using UnityEngine;

public class E05Attack : EnemyAttack {
    private E05Base e05Base;
    public E05Base E05Base {
        get {
            if (e05Base == null) {
                e05Base = EnemyBase as E05Base;
            }
            return e05Base;
        }
    }

    [SerializeField] private float delayAttack;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private float speedBullet;
    [SerializeField] private int numberBullet;
    [SerializeField] private float spreadAngle;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;

    GameLoader gameLoader;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberBullet);
        }
    }

    public override void Initialize() {
        base.Initialize();
        gameLoader = GameManager.Instance.GameLoader;
    }

    public override bool CanAttack() {
        return !isAttacking;
    }

    protected override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Shoting());
    }

    private IEnumerator Shoting() {
        yield return Yielder.Wait(delayAttack);
        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = Target.position - transform.position;
            FrontBullet centerBullet = gameLoader.SpawnBullet(bullet, transform.position);
            if (centerBullet) {
                centerBullet = ChangingBullet(centerBullet);
                centerBullet.SetSize(E05Base.E05Stat.Size.Value);
                centerBullet.Shoot(speedBullet, directionShot);
            }
            for (int ibullet = 0; ibullet < numberBullet / 2; ++ibullet) {
                Vector2 leftDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, spreadAngle * (ibullet + 1));
                FrontBullet leftBullet = gameLoader.SpawnBullet(bullet, transform.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    leftBullet.SetSize(E05Base.E05Stat.Size.Value);
                    leftBullet.Shoot(speedBullet, leftDirectionShot);
                }

                Vector2 rightDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * spreadAngle * (ibullet + 1));
                FrontBullet rightBullet = gameLoader.SpawnBullet(bullet, transform.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    rightBullet.SetSize(E05Base.E05Stat.Size.Value);
                    rightBullet.Shoot(speedBullet, rightDirectionShot);
                }
            }
            yield return Yielder.Wait(deltaShot);
        }
        yield return Yielder.Wait(1);
        EndAttack();
    }



    public void AimTarget() {
        E05Base.LookTarget();
    }
}
