using System.Collections;
using UnityEngine;
using Gemmob;

public class MB11Skill01AttackComponent : MinibossAttackComponent<MB11Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private LineRenderer warningLine;
    [SerializeField] private float h;
    [SerializeField] private ParticleSystem charge;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private float delayAfterAttack;
    [SerializeField] private int numberShot;
    [SerializeField] private float deltaShot;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float damagePercent;
    [SerializeField] private int numberBullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletAcceler;
    [SerializeField] private int numberPreload;

    bool isShoting;

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
        DrawWarning();
        yield return Yielder.Wait(delayAttack);
        isShoting = true;
        HideWarning();
        Vector2 directionShot = firePoint.up;
        for (int ishot = 0; ishot < numberShot; ++ishot) {
            if (muzzle) {
                muzzle.Play();
            }
            FrontBullet midBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
            if (midBullet) {
                midBullet = ChangingBullet(midBullet);
                midBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                midBullet.Shoot(bulletSpeed, directionShot);
            }
            for (int iBullet = 0; iBullet < numberBullet / 2; iBullet++) {
                Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, spreadAngle * (iBullet + 1));
                FrontBullet leftBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    leftBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                    leftBullet.Shoot(bulletSpeed, directionLeft);
                }


                Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * spreadAngle * (iBullet + 1));
                FrontBullet rightBullet = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    rightBullet.HitInfor.Damage.AddModifier(new StatModifier(damagePercent - 1, StatModType.PercentMult));
                    rightBullet.Shoot(bulletSpeed, directionRight);
                }
            }

            yield return Yielder.Wait(deltaShot);
        }
        isShoting = false;
        yield return Yielder.Wait(delayAfterAttack);
        EndAttack();
    }

    public override void StopAttack() {
        HideWarning();
        base.StopAttack();
        if (charge) {
            charge.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private void DrawWarning() {
        warningLine.SetPosition(0, Vector3.zero);
        warningLine.SetPosition(1, new Vector3(0, h, 1));
        float a = Mathf.Tan(spreadAngle * (numberBullet / 2) * Mathf.Deg2Rad) * 2 * h;
        warningLine.startWidth = 0;
        warningLine.endWidth = a;
        warningLine.gameObject.SetActive(true);
    }

    private void HideWarning() {
        warningLine.gameObject.SetActive(false);
    }


    private int atk;
    public override void StartAttack() {
        isShoting = false;
        atk = (int)(minibossAttack.MB11Base.MB11Stat.Atk.Value * damagePercent);
    }

    public override void Updating() {
        if (!isShoting) {
            minibossAttack.MB11Base.LookTarget();
        }
    }
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(atk, null, minibossAttack.MinibossBase);
        return bullet;
    }
}
