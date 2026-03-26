using UnityEngine;

public class ShotFrontBasicShipPattern : ShotFrontShipPattern {
    [SerializeField] protected ShotBasicPatternData patternData;
    [SerializeField] protected ParticleSystem muzzle;

    public override void Initialize() {
        base.Initialize();
    }

    protected override void DoAttacking() {
        ShotBasicPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotBasicPatternInfo>();
        Vector2 directionShot = Vector2.up;
        if (muzzle) {
            muzzle.Play();
        }
        FrontBullet bullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
        if (bullet) {
            bullet = ChangingBullet(bullet);
            bullet.Shoot(patternInfo.SpeedBullet, directionShot);
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
