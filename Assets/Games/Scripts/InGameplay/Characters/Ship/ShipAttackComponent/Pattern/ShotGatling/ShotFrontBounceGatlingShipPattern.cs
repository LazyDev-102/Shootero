using UnityEngine;

public class ShotFrontBounceGatlingShipPattern : ShotFrontGatlingShipPattern {
    private FrontBounceBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (FrontBounceBullet)shipAttackComponent.Bullet;
    }
    protected override void DoAttacking() {
        ShotGatlingPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGatlingPatternInfo>();
        Vector2 directionShot = Vector2.up;
        PlayShotEffect();
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet; ++ibullet) {
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, Random.Range(-patternInfo.SpreadAngle, patternInfo.SpreadAngle));
            FrontBounceBullet bullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (bullet) {
                bullet = ChangingBullet(bullet);
                bullet.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
                bullet.Shoot(patternInfo.SpeedBullet, directionRandom);
            }
        }
        EndAttacking();
    }

}
