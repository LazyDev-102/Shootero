using UnityEngine;

public class ShotFrontBounceXShipPattern : ShotFrontXShipPattern {
    private FrontBounceBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (FrontBounceBullet)shipAttackComponent.Bullet;
    }
    protected override void ShootBullet(ShotXPatternInfo info, Vector2 posA, Vector2 posB) {
        for (int ibullet = 0; ibullet < info.NumberBullet; ++ibullet) {
            Vector2 direction = Vector2.Lerp(posA, posB, (info.AngleStart + ibullet * info.AngleDistance) / defaultDistance);
            FrontBounceBullet bullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (bullet) {
                bullet = ChangingBullet(bullet);
                bullet.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
                bullet.Shoot(info.SpeedBullet, direction);
            }
        }
    }
}
