using UnityEngine;

public class ShotFrontPierceSingleStrikeShipPattern : ShotFrontShipPattern {
    [SerializeField] private ShotSingleStrikePatternData patternData;
    [SerializeField] private ParticleSystem muzzle;

    private Vector2 direction = Vector2.up;
    private PierceFrontBullet bulletPrefab;

    public override void Initialize() {
        base.Initialize();
        bulletPrefab = (PierceFrontBullet)shipAttackComponent.Bullet;
    }
    protected override void DoAttacking() {
        ShotSingleStrikePatternInfo patternInfo = GetCurrentShipPatternInfo<ShotSingleStrikePatternInfo>();
        PlayMuzzle();
        ShootBullet(patternInfo);
        EndAttacking();
    }
    private void PlayMuzzle() {
        if (muzzle) {
            muzzle.transform.position = FirePoint.position;
            muzzle.Play();
        }
    }
    private void ShootBullet(ShotSingleStrikePatternInfo info) {
        PierceFrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, FirePoint.position);
        if (goLeft) {
            goLeft = ChangingBullet(goLeft);
            goLeft.ChangeSpriteSize(info.BulletSize);
            goLeft.SetTimeFading(shipAttack.ShipBase.ShipStat.BulletFadeTimeLife.Value);
            goLeft.Shoot(info.BulletSpeed, direction);
        }
    }

    protected virtual PierceFrontBullet ChangingBullet(PierceFrontBullet bullet, ShotSingleStrikePatternInfo info) {
        var stat = shipAttack.ShipBase.ShipStat;
        var skill = shipAttack.ShipBase.ShipSkill;
        var critChance = stat.CritChance.Value + (int)(stat.CritChance.Value + info.CritRate);
        var critDamage = stat.CritDamage.Value + (int)(stat.CritDamage.Value + info.CritDamage);
        bullet.SpeedStat.SetBaseValue(stat.BulletSpeed.Value);
        bullet.Size.AddModifier(new StatModifier(stat.BulletSize.Value, StatModType.PercentAdd));
        bullet.SetHitInfor(stat.GetFinalDamageWeapon, skill.EffectAttackMods, shipAttack.ShipBase, critChance, critDamage);
        foreach (var mod in skill.ChangeBulletMods) {
            mod.ChangeBullet(bullet);
        }
        bullet.ChangeSize();
        return bullet;
    }

    protected override ShipPatternData GetShipPatternData() {
        return patternData;
    }

    protected override ShipPatternData<T> GetShipPatternData<T>() {
        return patternData as ShipPatternData<T>;
    }
}
