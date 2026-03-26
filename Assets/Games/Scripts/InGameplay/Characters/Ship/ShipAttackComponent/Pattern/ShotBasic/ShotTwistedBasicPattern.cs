using UnityEngine;

public class ShotTwistedBasicPattern : ShotTwistedShipPattern {
    [SerializeField] private ShotTwistedPlasmaPatternData patternData;
    [SerializeField] private ParticleSystem muzzleL;
    [SerializeField] private ParticleSystem muzzleR;
    private SinFrontBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = shipAttackComponent.FrontBullet;
    }

    protected override void DoAttacking() {
        ShotTwistedPlasmaPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotTwistedPlasmaPatternInfo>();
        PlayMuzzleEffect(patternInfo);
        ShotBullet(patternInfo);
        EndAttacking();
    }

    private void PlayMuzzleEffect(ShotTwistedPlasmaPatternInfo patternInfo) {
        if (muzzleL) {
            Vector2 position = (Vector2)FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase);
            muzzleL.transform.position = position;
            muzzleL.Play();
        }
        if (muzzleR) {
            Vector2 position = (Vector2)FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase);
            muzzleR.transform.position = position;
            muzzleR.Play();
        }
    }
    private void ShotBullet(ShotTwistedPlasmaPatternInfo patternInfo) {
        int length = patternInfo.NumberBullet / 2;
        Vector2 directionShot = Vector2.up;
        if (length <= 0)
            return;
        for (int ibullet = 0; ibullet < length; ++ibullet) {
            Vector2 positionLeft = (Vector2)FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            SinFrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, positionLeft);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
                goLeft.Shoot(patternInfo.SpeedBullet, directionShot, patternInfo.GetAmplitude(ibullet), patternInfo.GetCycle(ibullet), false);
            }
            Vector2 positionRight = (Vector2)FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            SinFrontBullet goRight = gameLoader.SpawnBullet(bulletPrefab, positionRight);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
                goRight.Shoot(patternInfo.SpeedBullet, directionShot, patternInfo.GetAmplitude(ibullet), patternInfo.GetCycle(ibullet));
            }
        }
    }
    protected override ShipPatternData GetShipPatternData() {
        return patternData;
    }

    protected override ShipPatternData<T> GetShipPatternData<T>() {
        return patternData as ShipPatternData<T>;
    }
}
