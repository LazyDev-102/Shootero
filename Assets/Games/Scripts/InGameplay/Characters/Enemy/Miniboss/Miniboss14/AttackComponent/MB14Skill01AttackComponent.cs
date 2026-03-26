using System.Collections;
using UnityEngine;
using Gemmob;

public class MB14Skill01AttackComponent : MinibossAttackComponent<MB14Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private SinBullet bullet;
    [SerializeField] private RangeFloatValue amplitudeRange;
    [SerializeField] private RangeFloatValue cycleRange;
    [SerializeField] private float distance;

    [SerializeField] private int numberShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShot;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float numberBullet;
    [SerializeField] private int numberPreload;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }

    }

    public override void Updating() {
        minibossAttack.MB14Base.LookTarget();
    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        yield return Yielder.Wait(delayAttack);
        for (int ishot = 0; ishot < numberShot; ++ishot) {
            Vector2 direction = firePoint.up;
            Vector2 positionMid = firePoint.position;
            SinBullet newBullet = GameLoader.SpawnBullet(bullet, positionMid);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet);
                newBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                newBullet.Shoot(bulletSpeed, direction, amplitudeRange.GetRandomValue(), cycleRange.GetRandomValue());
            }
            for (int ibullet = 0; ibullet < numberBullet / 2; ++ibullet) {

                Vector2 positionRight = firePoint.position + (ibullet + 1) * distance * firePoint.right;
                SinBullet newBulletRight = GameLoader.SpawnBullet(bullet, positionRight);
                if (newBulletRight) {
                    newBulletRight = ChangingBullet(newBulletRight);
                    newBulletRight.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                    newBulletRight.Shoot(bulletSpeed, direction, amplitudeRange.GetRandomValue(), cycleRange.GetRandomValue());
                }

                Vector2 positionLeft = firePoint.position + (ibullet + 1) * distance * firePoint.right * -1;
                SinBullet newBulletLeft = GameLoader.SpawnBullet(bullet, positionLeft);
                if (newBulletLeft) {
                    newBulletLeft = ChangingBullet(newBulletLeft);
                    newBulletLeft.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                    newBulletLeft.Shoot(bulletSpeed, direction, amplitudeRange.GetRandomValue(), cycleRange.GetRandomValue());
                }
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }

    private int atk;
    public override void StartAttack() {
        atk = (int)(minibossAttack.MB14Base.MB14Stat.Atk.Value * damagePercent);
    }
    public override void StopAttack() {
        base.StopAttack();
    }
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MB14Base);
        return bullet;
    }
}
