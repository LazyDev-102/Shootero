
using UnityEngine;

public class ShotFrontBoomSplitterShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotSplitterPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;
    private BoomTimeFrontBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (BoomTimeFrontBullet)shipAttackComponent.Bullet;
    }

    protected override void DoAttacking() {
        ShotSplitterPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotSplitterPatternInfo>();
        Vector2 directionShot = Vector2.up;
        if (muzzle) {
            muzzle.Play();
        }
        BoomTimeFrontBullet goMid = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (goMid) {
            goMid = ChangingBullet(goMid);
            goMid.SetBoomRadius(2)
                  .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                  .Shoot(patternInfo.SpeedBullet, directionShot, patternInfo.AccelerationSpeed, patternInfo.MinSpeed);
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, patternInfo.SpreadAngle * (ibullet + 1));
            BoomTimeFrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetBoomRadius(2)
                      .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                      .Shoot(patternInfo.SpeedBullet, directionLeft, patternInfo.AccelerationSpeed, patternInfo.MinSpeed);
            }
            Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * patternInfo.SpreadAngle * (ibullet + 1));
            BoomTimeFrontBullet goRight = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetBoomRadius(2)
                      .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                      .Shoot(patternInfo.SpeedBullet, directionRight, patternInfo.AccelerationSpeed, patternInfo.MinSpeed);
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
