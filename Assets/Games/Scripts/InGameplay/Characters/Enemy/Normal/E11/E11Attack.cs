
using Gemmob;
using Helper;
using System.Collections;
using UnityEngine;

public class E11Attack : EnemyAttack {
    private E11Base e11Base;
    public E11Base E11Base {
        get {
            if (e11Base == null) {
                e11Base = EnemyBase as E11Base;
            }
            return e11Base;
        }
    }

    [SerializeField] private float delayAttack;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private float speedBullet;
    [SerializeField] private int numberBullet;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private Transform firePos;
    [SerializeField] private RangeIntValue randomAngle;
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
        float deltaAngle = 360.0f / numberBullet;

        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShotBase = firePos.up.RotateDirection(randomAngle.GetRandomValue());
            for (int ibullet = 0; ibullet < numberBullet; ++ibullet) {
                Vector2 directionShot = directionShotBase.RotateDirection(deltaAngle * ibullet);
                FrontBullet newBullet = gameLoader.SpawnBullet(bullet, firePos.position);
                if (newBullet) {
                    newBullet = ChangingBullet(newBullet);
                    newBullet.SetSize(EnemyBase.EnemyStat.Size.Value);
                    newBullet.Shoot(speedBullet, directionShot);
                }
            }
            yield return Yielder.Wait(deltaShot);
        }
        yield return Yielder.Wait(0.5f);
        EndAttack();
    }


    public void AimTarget() {
        E11Base.LookTarget();
    }
}
