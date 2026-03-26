
using UnityEngine;

public class ShotFrontBoomXShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotXPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private float accelerationSpeed = -1;
    private readonly float defaultDistance = 90f;
    private BoomTimeFrontBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (BoomTimeFrontBullet)shipAttackComponent.Bullet;
    }
    protected override void DoAttacking() {
        ShotXPatternInfo patternInfo = GetCurrentShipPatternInfo<ShotXPatternInfo>();
        PlayMuzzle();
        ShootBullet(patternInfo, FirePoint.up, FirePoint.right);
        ShootBullet(patternInfo, FirePoint.up, FirePoint.right * -1);
        ShootBullet(patternInfo, FirePoint.up * -1, FirePoint.right);
        ShootBullet(patternInfo, FirePoint.up * -1, FirePoint.right * -1);
        EndAttacking();
    }
    private void PlayMuzzle() {
        if (muzzle) {
            muzzle.transform.position = FirePoint.position;
            muzzle.Play();
        }
    }
    private void ShootBullet(ShotXPatternInfo info, Vector2 posA, Vector2 posB) {
        for (int ibullet = 0; ibullet < info.NumberBullet; ++ibullet) {
            Vector2 direction = Vector2.Lerp(posA, posB, (info.AngleStart + ibullet * info.AngleDistance) / defaultDistance);
            BoomTimeFrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetBoomRadius(2)
                      .SetTimeAttackBoom(shipAttack.ShipBase.ShipStat.BulletTimeLife.Value)
                      .Shoot(info.SpeedBullet, direction, info.AccelerationSpeed, info.MinSpeed);
            }
        }
    }
    protected override ShipPatternData GetShipPatternData() {
        return patternData;
    }

    protected override ShipPatternData<T> GetShipPatternData<T>() {
        return patternData as ShipPatternData<T>;
    }
}
