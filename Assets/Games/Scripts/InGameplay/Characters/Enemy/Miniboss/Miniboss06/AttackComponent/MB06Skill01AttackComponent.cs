using System.Collections;
using UnityEngine;
using Helper;
using Gemmob;

public class MB06Skill01AttackComponent : MinibossAttackComponent<MB06Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private RangeFloatValue rangeSpreadAngle;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletAcceler;
    [SerializeField] private float bulletMinSpeed;
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
        for (int ishot = 0; ishot < numberShot; ++ishot) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShotBase = firePoint.up;
            Vector2 directionShot = directionShotBase.RotateDirection(rangeSpreadAngle.GetRandomValue());
            FrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                newBullet.Shoot(bulletSpeed, directionShot, bulletAcceler, bulletMinSpeed);
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }

    public override void Updating() {
        minibossAttack.MB06Base.LookTarget();
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private int atk;
    public override void StartAttack() {
        atk = (int)(minibossAttack.MB06Base.MB06Stat.Atk.Value * damagePercent);
    }

    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MB06Base);
        return bullet;
    }

}