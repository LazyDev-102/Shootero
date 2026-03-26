using UnityEngine;

public class ShotFrontPierceShotGunShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotGunPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;
    private PierceFrontBullet bulletPrefab;

    private Vector2 directionShot = Vector2.up;
    private float leftAngle;
    private float rightAngle;

    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (PierceFrontBullet)shipAttackComponent.Bullet;
    }

    protected override void DoAttacking() {
        Shot();
    }
    private void Shot() {
        ShotGunPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGunPatternInfo>();
        if (muzzle) {
            muzzle.Play();
        }
        PierceFrontBullet middleBullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (middleBullet) {
            middleBullet = ChangingBullet(middleBullet);
            middleBullet.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
            middleBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionShot);
        }
        leftAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            leftAngle -= patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, leftAngle);
            PierceFrontBullet leftBullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (leftBullet) {
                leftBullet = ChangingBullet(leftBullet);
                leftBullet.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
                leftBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom);
            }
        }
        rightAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            rightAngle += patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, rightAngle);
            PierceFrontBullet rightBullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (rightBullet) {
                rightBullet = ChangingBullet(rightBullet);
                rightBullet.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
                rightBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom);
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
