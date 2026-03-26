using UnityEngine;

public class ShotFrontPierceGatlingShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotGatlingPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;
    private PierceFrontBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (PierceFrontBullet)shipAttackComponent.Bullet;
    }

    protected override void DoAttacking() {
        ShotGatlingPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotGatlingPatternInfo>();
        Vector2 directionShot = Vector2.up;
        if (muzzle) {
            muzzle.Play();
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet; ++ibullet) {
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, Random.Range(-patternInfo.SpreadAngle, patternInfo.SpreadAngle));
            PierceFrontBullet bullet = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (bullet) {
                bullet = ChangingBullet(bullet);
                bullet.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
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
