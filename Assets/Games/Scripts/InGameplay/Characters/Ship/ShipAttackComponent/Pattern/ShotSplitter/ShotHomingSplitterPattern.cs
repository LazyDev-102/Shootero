using UnityEngine;

public class ShotHomingSplitterPattern : ShotHomingShipPattern {
    [SerializeField] private ShotSplitterPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;

    public override void Initialize() {
        base.Initialize();
    }

    protected override void DoAttacking() {
        ShotSplitterPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotSplitterPatternInfo>();
        Vector2 directionShot = Vector2.up;
        Transform targetMid = GetTargetMid(directionShot);
        if (muzzle) {
            muzzle.Play();
        }
        HomingBullet goMid = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
        if (goMid) {
            goMid = ChangingBullet(goMid);
            goMid.SetOwner(shipAttack.ShipBase);
            goMid.Shoot(patternInfo.SpeedBullet, targetMid, directionShot, findNextTarget: true);
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, patternInfo.SpreadAngle * (ibullet + 1));
            HomingBullet goLeft = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetOwner(shipAttack.ShipBase);
                goLeft.Shoot(patternInfo.SpeedBullet, GetTargetLeft(directionLeft), directionLeft, findNextTarget: true);
            }
            Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * patternInfo.SpreadAngle * (ibullet + 1));
            HomingBullet goRight = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
            if (goRight) {
                goRight = ChangingBullet(goRight);
                goRight.SetOwner(shipAttack.ShipBase);
                goRight.Shoot(patternInfo.SpeedBullet, GetTargetRight(directionRight), directionRight, findNextTarget: true);
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
