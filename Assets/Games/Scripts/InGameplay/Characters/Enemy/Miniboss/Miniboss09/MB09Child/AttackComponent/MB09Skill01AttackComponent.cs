using System.Collections;
using UnityEngine;
using Helper;
using Gemmob;
public class MB09Skill01AttackComponent : MinibossAttackComponent<MB09Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private RangeIntValue randomAngle;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private float delayAfterAttack;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private int numberBullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletAcceler;
    [SerializeField] private int numberPreload;


    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }

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

        float deltaAngle = 360.0f / numberBullet;
        for (int ishot = 0; ishot < numberShot; ++ishot) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShotBase = firePoint.up.RotateDirection(randomAngle.GetRandomValue());
            for (int ibullet = 0; ibullet < numberBullet; ++ibullet) {
                Vector2 directionShot = directionShotBase.RotateDirection(deltaAngle * ibullet);
                FrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (newBullet) {
                    newBullet = ChangingBullet(newBullet);
                    newBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                    newBullet.Shoot(bulletSpeed, directionShot, bulletAcceler);
                }
            }
            yield return Yielder.Wait(deltaShot);
        }
        yield return Yielder.Wait(delayAfterAttack);
        EndAttack();
    }

    public override void Updating() {
        minibossAttack.MB09Base.MB09Move.LookDirection(UnityHelper.Down);
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
    private int atk;
    public override void StartAttack() {
        atk = (int)(minibossAttack.MB09Base.MB09Stat.Atk.Value * damagePercent);
    }

    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MB09Base);
        return bullet;
    }

}