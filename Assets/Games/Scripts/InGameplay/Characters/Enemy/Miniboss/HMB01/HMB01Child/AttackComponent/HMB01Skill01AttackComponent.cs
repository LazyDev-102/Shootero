using System.Collections;
using UnityEngine;
using Gemmob;
using System.Collections.Generic;

public class HMB01Skill01AttackComponent : MinibossAttackComponent<HMB01Attack> {
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PierceFrontBullet bullet;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShotTime;
    [SerializeField] private int bulletCount;
    [SerializeField] private int attackCount;
    [SerializeField] private RangeFloatValue spreadAngle;
    [SerializeField] private RangeFloatValue speedBullet;
    [SerializeField] private int numberPreload;

    private bool canAim;
    private List<PierceFrontBullet> bullets = new List<PierceFrontBullet>();

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    public override void StartAttack() {
        if (bullets != null)
            bullets.Clear();
        atk = (int)(minibossAttack.HMB01Base.MinibossStat.Atk.Value * damagePercent);
        canAim = true;
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Shot());
    }

    private IEnumerator Shot() {
        yield return Yielder.Wait(0.5f);
        var directionShot = minibossAttack.Target.position - transform.position;
        for (int i = 0; i < attackCount; i++) {
            for (int ibullet = 0; ibullet < bulletCount; ++ibullet) {
                //Vector2 leftDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, attackData.SpreadAngle * (ibullet + 1));
                Vector2 leftDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, spreadAngle.GetRandomValue());
                PierceFrontBullet bulletClone = GameLoader.SpawnBullet(bullet, firePoint.position);
                if (bulletClone) {
                    bulletClone = ChangingBullet(bulletClone);
                    bulletClone.SetSize(minibossAttack.HMB01Base.MinibossStat.Size.Value);
                    bulletClone.Shoot(speedBullet.GetRandomValue(), leftDirectionShot);
                }
            }
            yield return Yielder.Wait(deltaShotTime);
        }
        EndAttack();
    }

    private int atk;
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor((int)(atk * damagePercent), null, minibossAttack.HMB01Base);
        return bullet;
    }

    public override void Updating() {
        if (canAim) {
            minibossAttack.HMB01Base.LookTarget();
        }
    }

}