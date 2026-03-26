using UnityEngine;

public class ShotFrontDoubleShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotDoublePatternData patternData;
    [SerializeField] private ParticleSystem muzzleL;
    [SerializeField] private ParticleSystem muzzleR;

    public override void Initialize() {
        base.Initialize();
    }
    protected virtual void PlayShotEffect(float distance) {
        if (muzzleL) {
            Vector2 position = (Vector2)FirePoint.position + Vector2.left * distance;
            muzzleL.transform.position = position;
            muzzleL.Play();
        }

        if (muzzleR) {
            Vector2 position = (Vector2)FirePoint.position + Vector2.right * distance;
            muzzleR.transform.position = position;
            muzzleR.Play();
        }
    }
    protected override void DoAttacking() {
        ShotDoublePatternInfo patternInfo = GetCurrentShipPatternInfo<ShotDoublePatternInfo>();
        Vector2 directionShot = Vector2.up;
        PlayShotEffect(patternInfo.HalfDistanceBase);
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 positionLeft = (Vector2)FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            FrontBullet goLeft = gameLoader.SpawnBullet(shipAttackComponent.Bullet, positionLeft);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.Shoot(patternInfo.SpeedBullet, directionShot);
            }
            Vector2 positionRight = (Vector2)FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            FrontBullet goRight = gameLoader.SpawnBullet(shipAttackComponent.Bullet, positionRight);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.Shoot(patternInfo.SpeedBullet, directionShot);
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
