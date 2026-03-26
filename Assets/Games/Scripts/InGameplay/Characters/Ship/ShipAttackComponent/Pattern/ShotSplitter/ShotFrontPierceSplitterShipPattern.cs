using UnityEngine;

public class ShotFrontPierceSplitterShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotSplitterPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;
    private PierceFrontBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (PierceFrontBullet)shipAttackComponent.Bullet;
    }

    protected override void DoAttacking() {
        ShotSplitterPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotSplitterPatternInfo>();
        Vector2 directionShot = Vector2.up;
        if (muzzle) {
            muzzle.Play();
        }
        PierceFrontBullet goMid = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (goMid) {
            goMid = ChangingBullet(goMid);
            goMid.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
            goMid.Shoot(patternInfo.SpeedBullet, directionShot);
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, patternInfo.SpreadAngle * (ibullet + 1));
            PierceFrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
                goLeft.Shoot(patternInfo.SpeedBullet, directionLeft);
            }
            Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * patternInfo.SpreadAngle * (ibullet + 1));
            PierceFrontBullet goRight = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
                goRight.Shoot(patternInfo.SpeedBullet, directionRight);
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
