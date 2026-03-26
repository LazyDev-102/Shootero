using UnityEngine;

public class ShotFrontBounceShotGunShipPattern : ShotFrontShotGunShipPattern {
    private FrontBounceBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (FrontBounceBullet)shipAttackComponent.Bullet;
    }
    protected override void Shot() {
        ShotGunPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGunPatternInfo>();
        PlayShotEffect();
        FrontBounceBullet middleBullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (middleBullet) {
            middleBullet = ChangingBullet(middleBullet);
            middleBullet.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
            middleBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionShot);
        }
        leftAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            leftAngle -= patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, leftAngle);
            FrontBounceBullet leftBullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (leftBullet) {
                leftBullet = ChangingBullet(leftBullet);
                leftBullet.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
                leftBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom);
            }
        }
        rightAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            rightAngle += patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, rightAngle);
            FrontBounceBullet rightBullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (rightBullet) {
                rightBullet = ChangingBullet(rightBullet);
                rightBullet.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
                rightBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom);
            }
        }
        EndAttacking();
    }
}
