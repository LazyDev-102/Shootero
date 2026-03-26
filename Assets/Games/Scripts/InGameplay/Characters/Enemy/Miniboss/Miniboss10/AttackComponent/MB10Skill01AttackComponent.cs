using System.Collections;
using UnityEngine;
using Gemmob;
public class MB10Skill01AttackComponent : MinibossAttackComponent<MB10Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AutoExplosionBullet bullet;
    [SerializeField] private Explosioner explosioner;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float damageBulletPercent;
    [SerializeField] private int numberBullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletAcceler;
    [SerializeField] private float timeLife;
    [SerializeField] private float radius;
    [SerializeField] private float damageExplosionPercent;
    [SerializeField] private int numberPreloadBullet;
    [SerializeField] private int numberPreloadExplosioner;

    bool isShoting;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreloadBullet);
        }
        if (explosioner) {
            explosioner.PreloadIngame();
            explosioner.RegisterPool(numberPreloadExplosioner);
        }
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    private IEnumerator IShotting() {
        yield return Yielder.Wait(delayAttack);
        isShoting = true;
        float lifeTime = timeLife;
        Vector2 directionShot = firePoint.up;
        for (int ishot = 0; ishot < numberShot; ++ishot) {
            AutoExplosionBullet midBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (midBullet) {
                midBullet = ChangingBullet(midBullet);
                midBullet.HitInfor.Damage.AddModifier(new StatModifier(damageBulletPercent - 1, StatModType.PercentMult));
                midBullet.AddOnDestroy(OnBulletExplosion);
                midBullet.Shoot(bulletSpeed, directionShot, bulletAcceler, timeLife: lifeTime);
                midBullet.SetAlpha(1);
            }
            for (int iBullet = 0; iBullet < numberBullet / 2; iBullet++) {
                Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, spreadAngle * (iBullet + 1));
                AutoExplosionBullet leftBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    leftBullet.HitInfor.Damage.AddModifier(new StatModifier(damageBulletPercent - 1, StatModType.PercentMult));
                    leftBullet.AddOnDestroy(OnBulletExplosion);
                    leftBullet.Shoot(bulletSpeed, directionLeft, bulletAcceler, timeLife: lifeTime);
                    leftBullet.SetAlpha(1);
                }

                Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * spreadAngle * (iBullet + 1));
                AutoExplosionBullet rightBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    rightBullet.HitInfor.Damage.AddModifier(new StatModifier(damageBulletPercent - 1, StatModType.PercentMult));
                    rightBullet.AddOnDestroy(OnBulletExplosion);
                    rightBullet.Shoot(bulletSpeed, directionRight, bulletAcceler, timeLife: lifeTime);
                    rightBullet.SetAlpha(1);
                }
            }
            yield return Yielder.Wait(deltaShot);
        }
        isShoting = false;
        EndAttack();
    }


    public override void Updating() {
        if (!isShoting) {
            minibossAttack.MB10Base.LookTarget();
        }
    }

    private void OnBulletExplosion(Vector3 position) {
        Explosioner newExplosioner = GameManager.Instance.GameLoader.SpawnExplosion(explosioner, position);
        if (newExplosioner) {
            var damage = minibossAttack == null ? 10 : minibossAttack.MB10Base.MB10Stat.Atk.Value * damageExplosionPercent;
            var causer = minibossAttack == null ? null : minibossAttack.MB10Base;
            newExplosioner.SetHitInfor((int)damage, null, causer)
                        .SetRadius(radius)
                        .Explosioning();
        }
    }

    private int atk;
    public override void StartAttack() {
        isShoting = false;
        atk = (int)(minibossAttack.MB10Base.MB10Stat.Atk.Value * damageBulletPercent);
    }
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MB10Base);
        return bullet;
    }

}