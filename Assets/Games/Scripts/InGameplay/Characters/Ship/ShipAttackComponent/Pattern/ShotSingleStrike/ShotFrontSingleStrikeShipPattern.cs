using UnityEngine;

public class ShotFrontSingleStrikeShipPattern : ShotFrontShipPattern {
    [SerializeField] protected ShotSingleStrikePatternData patternData;
    [SerializeField] protected ParticleSystem muzzle;

    protected Vector2 direction = Vector2.up;

    public override void Initialize() {
        base.Initialize();
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
    protected virtual void ShootBullet(ShotSingleStrikePatternInfo info) {
        FrontBullet goLeft = gameLoader.SpawnBullet(shipAttackComponent.Bullet, FirePoint.position);
        if (goLeft) {
            goLeft = ChangingBullet(goLeft, info);
            goLeft.ChangeSpriteSize(info.BulletSize);
            goLeft.Shoot(info.BulletSpeed, direction);
        }
    }

    protected virtual FrontBullet ChangingBullet(FrontBullet bullet, ShotSingleStrikePatternInfo info) {
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
