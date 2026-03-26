using UnityEngine;

public class ShotHomingGatlingPattern : ShotHomingShipPattern {
    [SerializeField] private ShotGatlingPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;

    public override void Initialize() {
        base.Initialize();
    }

    protected override void DoAttacking() {
        ShotGatlingPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGatlingPatternInfo>();
        Vector2 directionShot = Vector2.up;
        if (muzzle) {
            muzzle.Play();
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet; ++ibullet) {
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, Random.Range(-patternInfo.SpreadAngle, patternInfo.SpreadAngle));
            HomingBullet bullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
            if (bullet) {
                bullet = ChangingBullet(bullet);
                bullet.SetOwner(shipAttack.ShipBase);
                bullet.Shoot(patternInfo.SpeedBullet, GetTargetMid(directionRandom), directionRandom, findNextTarget: true);
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
