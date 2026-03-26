using Gemmob;
using System.Collections;
using UnityEngine;

public class ShotHomingShotGunPattern : ShotHomingShipPattern {
    [SerializeField] private ShotGunPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;

    private Vector2 directionShot = Vector2.up;

    public override void Initialize() {
        base.Initialize();
    }

    protected override void DoAttacking() {
        Shot();
        //if (gameObject.activeInHierarchy)
        //    StartCoroutine(Shotting());
    }
    private void Shot() {
        ShotGunPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGunPatternInfo>();
        if (muzzle) {
            muzzle.Play();
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet; ++ibullet) {
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, Random.Range(-patternInfo.SpreadAngle, patternInfo.SpreadAngle));
            HomingBullet bullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
            if (bullet) {
                bullet = ChangingBullet(bullet);
                bullet.SetOwner(shipAttack.ShipBase);
                bullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), GetTargetMid(directionRandom), directionRandom, findNextTarget: true);
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
    //        HomingBullet bullet = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
    //        if (bullet) {
    //            bullet = ChangingBullet(bullet);
    //            bullet.Shoot(patternInfo.SpeedBullet, GetTargetMid(directionRandom), directionRandom);
    //            yield return Yielder.Wait(Random.Range(patternInfo.MinAppearTime, patternInfo.MaxAppearTime));
    //        }
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
