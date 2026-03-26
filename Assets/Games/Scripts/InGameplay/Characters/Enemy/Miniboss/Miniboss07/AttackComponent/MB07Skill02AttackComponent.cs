using UnityEngine;
using System.Collections;
using Gemmob;

public class MB07Skill02AttackComponent : MinibossAttackComponent<MB07Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private RotateHomingBullet bullet;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaAttack;
    [SerializeField] private float timeHoming;
    [SerializeField] private float bulletSpeed;
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
            RotateHomingBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                newBullet.SetInfo(deltaAttack, timeHoming);
                newBullet.Shoot(bulletSpeed, minibossAttack.Target, directionShot);
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }

    public override void Updating() {
        minibossAttack.MB07Base.LookTarget();
    }

    public override void StopAttack() {
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private int atk;
    public override void StartAttack() {
        atk = (int)(minibossAttack.MB07Base.MB07Stat.Atk.Value * damagePercent);
    }

    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MB07Base);
        return bullet;
    }
}
