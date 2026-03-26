using UnityEngine;

public class ShotHomingTwistedPlasmaPattern : ShotHomingShipPattern {
    [SerializeField] private ShotTwistedPlasmaPatternData patternData;
    [SerializeField] private ParticleSystem muzzleL;
    [SerializeField] private ParticleSystem muzzleR;
    private SinHomingBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = shipAttackComponent.SinHomingBullet;
    }

    protected override void DoAttacking() {
        if (bulletPrefab == null)
            return;
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
            SinHomingBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, positionLeft);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetOwner(shipAttack.ShipBase);
                goLeft.Shoot(patternInfo.SpeedBullet, GetTargetLeft(directionShot), directionShot);
            }
            Vector2 positionRight = (Vector2)FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            SinHomingBullet goRight = gameLoader.SpawnBullet(bulletPrefab, positionRight);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetOwner(shipAttack.ShipBase);
                goRight.Shoot(patternInfo.SpeedBullet, GetTargetRight(directionShot), directionShot, 0, false);
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
