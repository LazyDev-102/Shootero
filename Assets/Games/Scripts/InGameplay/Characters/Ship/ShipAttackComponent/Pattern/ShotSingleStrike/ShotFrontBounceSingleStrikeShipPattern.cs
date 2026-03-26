using UnityEngine;

public class ShotFrontBounceSingleStrikeShipPattern : ShotFrontSingleStrikeShipPattern {
    private FrontBounceBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (FrontBounceBullet)shipAttackComponent.Bullet;
    }
    protected override void ShootBullet(ShotSingleStrikePatternInfo info) {
        FrontBounceBullet bullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (bullet) {
            bullet = ChangingBullet(bullet);
            bullet.ChangeSpriteSize(info.BulletSize);
            bullet.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
            bullet.Shoot(info.BulletSpeed, direction);
        }
    }
}
