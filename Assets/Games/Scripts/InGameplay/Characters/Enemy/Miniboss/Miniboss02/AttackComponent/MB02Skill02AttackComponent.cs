using System.Collections;
using UnityEngine;
using Gemmob;


public class MB02Skill02AttackComponent : MinibossAttackComponent<MB02Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;
    [Header("Data")]
    [SerializeField] private int numberShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShot;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float accelerBullet;
    [SerializeField] private float minSpeedBullet;
    [SerializeField] private float spreadAngle;
    [SerializeField] private int numberBullet;


    private int atk;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }

    }
    public override void StartAttack() {
        atk = (int)(minibossAttack.MinibossBase.MinibossStat.Atk.Value * damagePercent);
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
        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = firePoint.up;
            FrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.Shoot(bulletSpeed, directionShot, accelerBullet, minSpeedBullet);
            }
            for (int ibullet = 0; ibullet < numberBullet / 2; ++ibullet) {
                Vector2 leftDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, spreadAngle * (ibullet + 1));
                FrontBullet leftBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    leftBullet.Shoot(bulletSpeed, leftDirectionShot, accelerBullet, minSpeedBullet);
                }

                Vector2 rightDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * spreadAngle * (ibullet + 1));
                FrontBullet rightBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    rightBullet.Shoot(bulletSpeed, rightDirectionShot, accelerBullet, minSpeedBullet);
                }
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }

    public override void Updating() {
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MinibossBase);
        return bullet;
    }
}
