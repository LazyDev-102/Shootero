using Gemmob;
using System.Collections;
using UnityEngine;

public class ShotFrontShotGunShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotGunPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;

    protected Vector2 directionShot = Vector2.up;
    protected float leftAngle;
    protected float rightAngle;

    public override void Initialize() {
        base.Initialize();
    }

    protected override void DoAttacking() {
        Shot();
        //if (gameObject.activeInHierarchy)
        //    StartCoroutine(Shotting());
    }
    protected virtual void PlayShotEffect() {
        if (muzzle) {
            muzzle.Play();
        }
    }
    protected virtual void Shot() {
        ShotGunPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGunPatternInfo>();
        PlayShotEffect();
        // 1 bullet in middle
        FrontBullet middleBullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
        if (middleBullet) {
            middleBullet = ChangingBullet(middleBullet);
            middleBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionShot);
        }
        // bullet left
        leftAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            leftAngle -= patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, leftAngle);
            FrontBullet leftBullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
            if (leftBullet) {
                leftBullet = ChangingBullet(leftBullet);
                leftBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom);
            }
        }
        // bullet right
        rightAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            rightAngle += patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, rightAngle);
            FrontBullet rightBullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
            if (rightBullet) {
                rightBullet = ChangingBullet(rightBullet);
                rightBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom);
            }
        }
        EndAttacking();
    }
    //private IEnumerator Shotting() {
    //    ShotGunPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGunPatternInfo>();
    //    Vector2 directionShot = Vector2.up;
    //    if (muzzle) {
    //        muzzle.Play();
    //    }
    //    for (int ibullet = 0; ibullet < patternInfo.NumberBullet; ++ibullet) {
    //        Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, Random.Range(-patternInfo.SpreadAngle, patternInfo.SpreadAngle));
    //        FrontBullet bullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
    //        if (bullet) {
    //            bullet = ChangingBullet(bullet);
    //            bullet.Shoot(patternInfo.SpeedBullet, directionRandom);
    //        }
    //        yield return Yielder.Wait(Random.Range(patternInfo.MinAppearTime, patternInfo.MaxAppearTime));
    //    }
    //    EndAttacking();
    //}
    protected override ShipPatternData GetShipPatternData() {
        return patternData;
    }

    protected override ShipPatternData<T> GetShipPatternData<T>() {
        return patternData as ShipPatternData<T>;
    }
}
