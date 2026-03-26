using UnityEngine;

public class ShotFrontSplitterShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotSplitterPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;
    public override void Initialize() {
        base.Initialize();
    }
    protected void PlayShotEffect() {
        if (muzzle) {
            muzzle.Play();
        }
    }
    protected override void DoAttacking() {
        ShotSplitterPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotSplitterPatternInfo>();
        Vector2 directionShot = Vector2.up;
        PlayShotEffect();
        FrontBullet goMid = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
        if (goMid) {
            goMid = ChangingBullet(goMid);
            goMid.Shoot(patternInfo.SpeedBullet, directionShot);
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, patternInfo.SpreadAngle * (ibullet + 1));
            FrontBullet goLeft = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.Shoot(patternInfo.SpeedBullet, directionLeft);
            }
            Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * patternInfo.SpreadAngle * (ibullet + 1));
            FrontBullet goRight = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
            if (goRight) {
                goRight = ChangingBullet(goRight);
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
