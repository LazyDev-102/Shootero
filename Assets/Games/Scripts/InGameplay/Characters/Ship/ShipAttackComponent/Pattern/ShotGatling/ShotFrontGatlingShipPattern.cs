using UnityEngine;

public class ShotFrontGatlingShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotGatlingPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;
    public override void Initialize() {
        base.Initialize();
    }
    protected virtual void PlayShotEffect() {
        if (muzzle) {
            muzzle.Play();
        }
    }
    protected override void DoAttacking() {
        ShotGatlingPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGatlingPatternInfo>();
        Vector2 directionShot = Vector2.up;
        PlayShotEffect();
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet; ++ibullet) {
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, Random.Range(-patternInfo.SpreadAngle, patternInfo.SpreadAngle));
            FrontBullet bullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
            if (bullet) {
                bullet = ChangingBullet(bullet);
                bullet.Shoot(patternInfo.SpeedBullet, directionRandom);
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
