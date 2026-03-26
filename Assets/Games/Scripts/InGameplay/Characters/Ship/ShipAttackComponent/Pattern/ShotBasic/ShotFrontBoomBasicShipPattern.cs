
using UnityEngine;

public class ShotFrontBoomBasicShipPattern : ShotFrontBasicShipPattern {
    [SerializeField] private float accelerationSpeed = -1;
    [SerializeField] private float minSpeed = 1;

    private BoomTimeFrontBullet bulletPrefab = null;

    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (BoomTimeFrontBullet)shipAttackComponent.Bullet;
    }
    protected override void DoAttacking() {
        ShotBasicPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotBasicPatternInfo>();
        Vector2 directionShot = Vector2.up;
        if (muzzle) {
            muzzle.Play();
        }
        BoomTimeFrontBullet bullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (bullet) {
            bullet = ChangingBullet(bullet);
            bullet.SetBoomRadius(2)
                  .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                  .Shoot(patternInfo.SpeedBullet, directionShot, accelerationSpeed, minSpeed);
        }
        EndAttacking();
    }
}
