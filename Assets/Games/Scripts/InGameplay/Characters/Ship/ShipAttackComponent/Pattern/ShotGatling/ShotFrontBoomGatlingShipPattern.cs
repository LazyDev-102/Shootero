
using UnityEngine;

public class ShotFrontBoomGatlingShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotGatlingPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;
    private BoomTimeFrontBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (BoomTimeFrontBullet)shipAttackComponent.Bullet;
    }

    protected override void DoAttacking() {
        ShotGatlingPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGatlingPatternInfo>();
        Vector2 directionShot = Vector2.up;
        if (muzzle) {
            muzzle.Play();
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet; ++ibullet) {
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, Random.Range(-patternInfo.SpreadAngle, patternInfo.SpreadAngle));
            BoomTimeFrontBullet bullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (bullet) {
                bullet = ChangingBullet(bullet);
                bullet.SetBoomRadius(2)
                      .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                      .Shoot(patternInfo.SpeedBullet, directionRandom, patternInfo.AccelerationSpeed, patternInfo.MinSpeed);
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
