using UnityEngine;


public class ShotHomingBasicPattern : ShotHomingShipPattern {
    [SerializeField] private ShotBasicPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;

    public override void Initialize() {
        base.Initialize();
    }

    protected override void DoAttacking() {
        ShotBasicPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotBasicPatternInfo>();
        Vector2 directionShot = Vector2.up;
        Transform targetMid = GetTargetMid(directionShot);
        if (muzzle) {
            muzzle.Play();
        }
        HomingBullet bullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
        if (bullet) {
            bullet = ChangingBullet(bullet);
            bullet.SetOwner(shipAttack.ShipBase);
            bullet.Shoot(patternInfo.SpeedBullet, targetMid, directionShot, findNextTarget: true);
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
