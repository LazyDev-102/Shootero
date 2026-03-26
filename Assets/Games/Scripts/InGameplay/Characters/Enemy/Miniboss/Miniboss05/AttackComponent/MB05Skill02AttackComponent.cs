using System.Collections;
using UnityEngine;
using Gemmob;

public class MB05Skill02AttackComponent : MinibossAttackComponent<MB05Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShot;
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


    public override void Updating() {
        minibossAttack.MB05Base.LookTarget();
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
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                newBullet.Shoot(bulletSpeed, directionShot, bulletAcceler, bulletMinSpeed);
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
    private int atk;
    public override void StartAttack() {
        atk = (int)(minibossAttack.MB05Base.MB05Stat.Atk.Value * damagePercent);
    }

    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MB05Base);
        return bullet;
    }
}