using UnityEngine;

public class ShotFrontBounceBasicShipPattern : ShotFrontBasicShipPattern {
    private FrontBounceBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (FrontBounceBullet)shipAttackComponent.Bullet;
    }
    protected override void DoAttacking() {
        ShotBasicPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotBasicPatternInfo>();
        Vector2 directionShot = Vector2.up;
        if (muzzle) {
            muzzle.Play();
        }
        FrontBounceBullet bullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (bullet) {
            bullet = ChangingBullet(bullet);
            bullet.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
            bullet.Shoot(patternInfo.SpeedBullet, directionShot);
        }
        EndAttacking();
    }
}
