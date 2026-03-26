using UnityEngine;

public class ShotPierceFrontBasicShipPattern : ShotFrontBasicShipPattern {
    private PierceFrontBullet bulletPrefab = null;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (PierceFrontBullet)shipAttackComponent.Bullet;
    }
    protected override void DoAttacking() {
        ShotBasicPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotBasicPatternInfo>();
        Vector2 directionShot = Vector2.up;
        if (muzzle) {
            muzzle.Play();
        }
        PierceFrontBullet bullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (bullet) {
            bullet = ChangingBullet(bullet);
            bullet.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
            bullet.Shoot(patternInfo.SpeedBullet, directionShot);
        }
        EndAttacking();
    }
}
