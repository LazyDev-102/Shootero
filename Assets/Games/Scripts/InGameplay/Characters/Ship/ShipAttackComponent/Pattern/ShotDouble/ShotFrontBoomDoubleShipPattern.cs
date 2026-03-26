
using UnityEngine;

public class ShotFrontBoomDoubleShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotDoublePatternData patternData;
    [SerializeField] private ParticleSystem muzzleL;
    [SerializeField] private ParticleSystem muzzleR;

    private BoomTimeFrontBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (BoomTimeFrontBullet)shipAttackComponent.Bullet;
    }
    private void PlayEffect(ShotDoublePatternInfo patternInfo) {
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
    protected override void DoAttacking() {
        ShotDoublePatternInfo patternInfo = GetCurrentShipPatternInfo<ShotDoublePatternInfo>();
        Vector2 directionShot = Vector2.up;
        PlayEffect(patternInfo);
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 positionLeft = (Vector2)FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            BoomTimeFrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, positionLeft);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetBoomRadius(2)
                      .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                      .Shoot(patternInfo.SpeedBullet, directionShot, patternInfo.AccelerationSpeed, patternInfo.MinSpeed);
            }
            Vector2 positionRight = (Vector2)FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            BoomTimeFrontBullet goRight = gameLoader.SpawnBullet(bulletPrefab, positionRight);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetBoomRadius(2)
                      .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                      .Shoot(patternInfo.SpeedBullet, directionShot, patternInfo.AccelerationSpeed, patternInfo.MinSpeed);
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
