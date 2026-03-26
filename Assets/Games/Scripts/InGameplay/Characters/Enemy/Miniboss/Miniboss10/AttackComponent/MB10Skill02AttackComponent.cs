using System.Collections;
using UnityEngine;
using Gemmob;


public class MB10Skill02AttackComponent : MinibossAttackComponent<MB10Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineLightningBullet bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int numberShot;
    [SerializeField] private float damageCirclePercent;
    [SerializeField] private float damageLinePercent;
    [SerializeField] private float deltaShot;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }

    }

    public override void StartAttack() {
    }

    public override void Updating() {
        minibossAttack.MB10Base.LookTarget();
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
        int damage = minibossAttack.MB10Base.MB10Stat.Atk.Value;
        int damageCircle = (int)(damage * damageCirclePercent);
        int damageLine = (int)(damage * damageLinePercent);

        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = firePoint.up;
            LineLightningBullet newBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (newBullet) {
                newBullet.SetInfor(damageCircle, damageLine, minibossAttack.MB10Base);
                newBullet.Shoot(bulletSpeed, directionShot);
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
}
