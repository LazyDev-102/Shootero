using System.Collections;
using UnityEngine;
using Gemmob;
public class HMB02Skill01AttackComponent : MinibossAttackComponent<HMB02Attack> {
    [SerializeField] private Transform firePoint;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberShot;
    [SerializeField] private float damagePercent;
    [SerializeField] private float deltaShot;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletCount;
    [SerializeField] private float accelerBullet;
    [SerializeField] private float minSpeedBullet;
    [SerializeField] private float distanceBase;
    [SerializeField] private float distance;
    [SerializeField] private float distanceY;

    [SerializeField] private int numberPreload;

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    public override void StartAttack() {

    }
    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IShotting());
    }

    public override void Updating() {
        minibossAttack.HMB02Base.LookTarget();
    }

    private IEnumerator IShotting() {
        int damage = (int)(minibossAttack.HMB02Base.MinibossStat.Atk.Value * damagePercent);
        for (int i = 0; i < numberShot; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
        var originPos = firePoint.position;
        var normalLizeX = transform.right.normalized;
        var normalLizeY = transform.up.normalized;
        Vector2 directionShot = minibossAttack.Target.position - firePoint.position;
            for (int j = 0; j < bulletCount / 2; j++) {
                var offsetX = j==0? normalLizeX * (j + 1) * distanceBase : normalLizeX * (j + 1) * distance;
                var offsetY = normalLizeY * (j * distanceY);
                FrontBullet leftBullet = GameLoader.SpawnBullet(bullet, originPos - offsetX - offsetY);
                if (leftBullet) {
                    leftBullet.SetHitInfor(damage, null, minibossAttack.HMB02Base);
                    leftBullet.Shoot(bulletSpeed, directionShot, accelerBullet, minSpeedBullet);
                }
                FrontBullet rightBullet = GameLoader.SpawnBullet(bullet, originPos + offsetX - offsetY);
                if (rightBullet) {
                    rightBullet.SetHitInfor(damage, null, minibossAttack.HMB02Base);
                    rightBullet.Shoot(bulletSpeed, directionShot, accelerBullet, minSpeedBullet);
                }
            }
            yield return Yielder.Wait(deltaShot);
        }
        EndAttack();
    }


}