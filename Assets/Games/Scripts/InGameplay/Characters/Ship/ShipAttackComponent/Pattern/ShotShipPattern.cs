

public abstract class ShotShipPattern<T> : ShipPattern<T> where T : ShotShipAttackComponent {
    ShipSkill shipSkill;
    ShipStat shipStat;


    ShipSkill ShipSkill {
        get {
            if (shipSkill == null) {
                shipSkill = shipAttack.ShipBase.ShipSkill;
            }
            return shipSkill;
        }
    }

    ShipStat ShipStat {
        get {
            if (shipStat == null) {
                shipStat = shipAttack.ShipBase.ShipStat;
            }
            return shipStat;
        }
    }

    protected virtual U ChangingBullet<U>(U bullet) where U : BulletBase {
        bullet.SpeedStat.SetBaseValue(ShipStat.BulletSpeed.Value);
        bullet.Size.AddModifier(new StatModifier(ShipStat.BulletSize.Value, StatModType.PercentAdd));
        bullet.SetHitInfor(ShipStat.GetFinalDamageWeapon, ShipSkill.EffectAttackMods, shipAttack.ShipBase, ShipStat.CritChance.Value, ShipStat.CritDamage.Value);
        foreach (var mod in ShipSkill.ChangeBulletMods) {
            mod.ChangeBullet(bullet);
        }
        bullet.ChangeSize();
        return bullet;
    }
}

