using System.Collections;
using UnityEngine;
using Gemmob;

public class MB04Skill01AttackComponent : MinibossAttackComponent<MB04Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private HomingBullet bullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;
    [Header("Data")]
    [SerializeField] private int numberShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShot;
    [SerializeField] private int numberBullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float accelerBullet;
    [SerializeField] private float delayHoming;
    [SerializeField] private float spreadAngle;

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
        Transform target = minibossAttack.Target;
        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = firePoint.up;

            for (int iBullet = 0; iBullet < numberBullet / 2; iBullet++) {
                Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, spreadAngle * (iBullet + 1));
                HomingBullet leftBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    leftBullet.SetDelayHoming(delayHoming);
                    leftBullet.Shoot(bulletSpeed, target, directionLeft, accelerBullet);
                }
                Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * spreadAngle * (iBullet + 1));
                HomingBullet rightBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    rightBullet.SetDelayHoming(delayHoming);
                    rightBullet.Shoot(bulletSpeed, target, directionRight, accelerBullet);
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
