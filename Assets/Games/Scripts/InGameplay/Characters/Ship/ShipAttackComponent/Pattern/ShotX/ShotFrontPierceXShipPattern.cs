using UnityEngine;

public class ShotFrontPierceXShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotXPatternData patternData;
    [SerializeField] private ParticleSystem muzzle;
    private readonly float defaultDistance = 90f;
    private PierceFrontBullet bulletPrefab;
    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (PierceFrontBullet)shipAttackComponent.Bullet;
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
            PierceFrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
            if (goLeft) {
                goLeft = ChangingBullet(goLeft);
                goLeft.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
                goLeft.Shoot(info.SpeedBullet, direction);
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
