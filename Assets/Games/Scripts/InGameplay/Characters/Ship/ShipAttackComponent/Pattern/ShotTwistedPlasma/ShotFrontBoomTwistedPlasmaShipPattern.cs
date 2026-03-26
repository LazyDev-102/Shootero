
using UnityEngine;

public class ShotFrontBoomTwistedPlasmaShipPattern : ShotFrontTwistedPlasmaShipPattern {

    private BoomTimeSinFrontBullet bPrefab;
    public override void Initialize() {
        base.Initialize();
        bPrefab = (BoomTimeSinFrontBullet)shipAttackComponent.FrontBullet;
    }

    protected override void ShotBullet(ShotTwistedPlasmaPatternInfo patternInfo) {
        int length = patternInfo.NumberBullet / 2;
        Vector2 directionShot = Vector2.up;
        if (length <= 0)
            return;
        for (int ibullet = 0; ibullet < length; ++ibullet) {
            Vector2 positionLeft = (Vector2)FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            BoomTimeSinFrontBullet goLeft = gameLoader.SpawnBullet(bPrefab, positionLeft);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetBoomRadius(2)
                      .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                      .Shoot(patternInfo.SpeedBullet, directionShot, patternInfo.GetAmplitude(ibullet), patternInfo.GetCycle(ibullet), false);
            }
            Vector2 positionRight = (Vector2)FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            BoomTimeSinFrontBullet goRight = gameLoader.SpawnBullet(bPrefab, positionRight);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetBoomRadius(2)
                       .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                       .Shoot(patternInfo.SpeedBullet, directionShot, patternInfo.GetAmplitude(ibullet), patternInfo.GetCycle(ibullet));
            }
        }
    }
}
