using UnityEngine;

public class ShotFrontBounceDoubleShipPattern : ShotFrontDoubleShipPattern {
    private FrontBounceBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (FrontBounceBullet)shipAttackComponent.Bullet;
    }
    protected override void DoAttacking() {
        ShotDoublePatternInfo patternInfo = GetCurrentShipPatternInfo<ShotDoublePatternInfo>();
        Vector2 directionShot = Vector2.up;
        PlayShotEffect(patternInfo.HalfDistanceBase);
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 positionLeft = (Vector2)FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            FrontBounceBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, positionLeft);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
                goLeft.Shoot(patternInfo.SpeedBullet, directionShot);
            }
            Vector2 positionRight = (Vector2)FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            FrontBounceBullet goRight = gameLoader.SpawnBullet(bulletPrefab, positionRight);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetBounceCount(shipAttack.ShipBase.ShipStat.Bounce.Value);
                goRight.Shoot(patternInfo.SpeedBullet, directionShot);
            }
        }

        EndAttacking();
    }
}
