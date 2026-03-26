using UnityEngine;

public class ShotFrontPierceTwistedPlasmaShipPattern : ShotFrontTwistedPlasmaShipPattern {
    private PierceSinFrontBullet pierceBulletPrefab;
    public override void Initialize() {
        base.Initialize();
        pierceBulletPrefab = (PierceSinFrontBullet)shipAttackComponent.FrontBullet;
    }

    protected override void ShotBullet(ShotTwistedPlasmaPatternInfo patternInfo) {
        int length = patternInfo.NumberBullet / 2;
        Vector2 directionShot = Vector2.up;
        if (length <= 0)
            return;
        for (int ibullet = 0; ibullet < length; ++ibullet) {
            Vector2 positionLeft = (Vector2)FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            PierceSinFrontBullet goLeft = gameLoader.SpawnBullet(pierceBulletPrefab, positionLeft);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
                goLeft.Shoot(patternInfo.SpeedBullet, directionShot, patternInfo.GetAmplitude(ibullet), patternInfo.GetCycle(ibullet), false);
            }
            Vector2 positionRight = (Vector2)FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            PierceSinFrontBullet goRight = gameLoader.SpawnBullet(pierceBulletPrefab, positionRight);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
                goRight.Shoot(patternInfo.SpeedBullet, directionShot, patternInfo.GetAmplitude(ibullet), patternInfo.GetCycle(ibullet));
            }
        }
    }
}
