using System.Collections;
using UnityEngine;
using Gemmob;

public class MB08Skill02AttackComponent : MinibossAttackComponent<MB08Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RotateFrontBullet bullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaAttack;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletSize;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateAcceler;
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
        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = firePoint.up;
            RotateFrontBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                newBullet.Shoot(directionShot, bulletSpeed);
                newBullet.SetInfo(deltaAttack);
                newBullet.SetSize(bulletSize);
                newBullet.SetRotateSpeed(rotateSpeed, rotateAcceler);
            }

            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }

    public override void Updating() {
        minibossAttack.MB08Base.LookTarget();
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private int atk;

    public override void StartAttack() {
        atk = (int)(minibossAttack.MinibossBase.MinibossStat.Atk.Value * damagePercent);
    }

    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MinibossBase);
        return bullet;
    }
}
