using UnityEngine;

public class ShotHomingDoublePattern : ShotHomingShipPattern {
    [SerializeField] private ShotDoublePatternData patternData;
    [SerializeField] private ParticleSystem muzzleL;
    [SerializeField] private ParticleSystem muzzleR;

    public override void Initialize() {
        base.Initialize();
    }

    protected override void DoAttacking() {
        ShotDoublePatternInfo patternInfo = GetCurrentShipPatternInfo<ShotDoublePatternInfo>();
        Vector2 directionShot = Vector2.up;
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
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 positionLeft = (Vector2)FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            HomingBullet goLeft = gameLoader.SpawnBullet(shipAttackComponent.Bullet, positionLeft);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetOwner(shipAttack.ShipBase);
                goLeft.Shoot(patternInfo.SpeedBullet, GetTargetLeft(directionShot), directionShot, findNextTarget: true);
            }
            Vector2 positionRight = (Vector2)FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            HomingBullet goRight = gameLoader.SpawnBullet(shipAttackComponent.Bullet, positionRight);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetOwner(shipAttack.ShipBase);
                goRight.Shoot(patternInfo.SpeedBullet, GetTargetRight(directionShot), directionShot, findNextTarget: true);
            }
        }

        EndAttacking();
    }

    protected override ShipPatternData GetShipPatternData() {
        return patternData;
    }

    protected override ShipPatternData<T> GetShipPatternData<T>() {
        return patternData as ShipPatternData<T>;
    }

}
