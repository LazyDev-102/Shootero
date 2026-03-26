
using UnityEngine;

public class ShotFrontBoomShotGunShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotGunPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;

    private BoomTimeFrontBullet bulletPrefab;
    private Vector2 directionShot = Vector2.up;
    private float leftAngle;
    private float rightAngle;

    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (BoomTimeFrontBullet)shipAttackComponent.Bullet;
    }

    protected override void DoAttacking() {
        Shot();
    }
    private void Shot() {
        ShotGunPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGunPatternInfo>();
        if (muzzle) {
            muzzle.Play();
        }
        BoomTimeFrontBullet middleBullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (middleBullet) {
            middleBullet = ChangingBullet(middleBullet);
            middleBullet.SetBoomRadius(2)
                  .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                  .Shoot(patternInfo.SpeedRange.GetRandomValue(), directionShot, patternInfo.AccelerationSpeed, patternInfo.MinSpeed);
        }
        leftAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            leftAngle -= patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, leftAngle);
            BoomTimeFrontBullet leftBullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (leftBullet) {
                leftBullet = ChangingBullet(leftBullet);
                leftBullet.SetBoomRadius(2)
                          .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                          .Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom, patternInfo.AccelerationSpeed, patternInfo.MinSpeed);
            }
        }
        rightAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            rightAngle += patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, rightAngle);
            BoomTimeFrontBullet rightBullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (rightBullet) {
                rightBullet = ChangingBullet(rightBullet);
                rightBullet.SetBoomRadius(2)
                           .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                           .Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom, patternInfo.AccelerationSpeed, patternInfo.MinSpeed);
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
