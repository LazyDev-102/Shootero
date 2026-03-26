using UnityEngine;

public class ShotFrontBounceSplitterShipPattern : ShotFrontSplitterShipPattern {
    private FrontBounceBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (FrontBounceBullet)shipAttackComponent.Bullet;
    }
    protected override void DoAttacking() {
        ShotSplitterPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotSplitterPatternInfo>();
        Vector2 directionShot = Vector2.up;
        PlayShotEffect();
        FrontBounceBullet goMid = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (goMid) {
            goMid = ChangingBullet(goMid);
            goMid.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
            goMid.Shoot(patternInfo.SpeedBullet, directionShot);
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, patternInfo.SpreadAngle * (ibullet + 1));
            FrontBounceBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
                goLeft.Shoot(patternInfo.SpeedBullet, directionLeft);
            }
            Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * patternInfo.SpreadAngle * (ibullet + 1));
            FrontBounceBullet goRight = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
                goRight.Shoot(patternInfo.SpeedBullet, directionRight);
            }
        }
        EndAttacking();
    }
}
