using UnityEngine;


[CreateAssetMenu(fileName = "TurretShieldModData", menuName = "Mod/Buff/Limited/TurretShield")]
public class TurretShieldModData : BuffLimitStackModData {
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        ShieldEffect shipSeftEffect = character.ShipSkill.GetSelfEffect<ShieldEffect>(ShieldEffect.shiledId);
        EnergyShieldEffect energyShieldEffect = character.ShipSkill.GetSelfEffect<EnergyShieldEffect>(EnergyShieldEffect.shieldId);
        var isProtecShield = shipSeftEffect != null;
        character.ShipHitbox.TurnOffShield(isProtecShield);
        //character.ShipSkill.RemoveSelfEffect(isProtecShield ? shipSeftEffect : energyShieldEffect as ShipSeflEffect);
        if (isProtecShield)
            shipSeftEffect.PauseEffect(true);
        else
            energyShieldEffect.PauseEffect(true);
        TurretModInfo turretInfo = character.ShipSkill.GetTurretModInfo();
        if (turretInfo != null) {
            if (isProtecShield) {
                turretInfo.TransferShield(character, shipSeftEffect.ShieldDurantion, shipSeftEffect.ShieldCountdown, shipSeftEffect != null, 0, 0);
            }
            else {
                turretInfo.TransferShield(character, energyShieldEffect.TimeReborn, energyShieldEffect.TimeReborn, shipSeftEffect != null, energyShieldEffect.HP.Value, energyShieldEffect.DodgeRate.Value);
            }
        }
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

