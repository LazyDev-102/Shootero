using UnityEngine;


[CreateAssetMenu(fileName = "TurretCountdownModData", menuName = "Mod/Buff/Limited/TurretCountdown")]
public class TurretCountdownModData : BuffLimitStackModData {
    [SerializeField] private StatModifier timeReborn;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        TurretModInfo turretInfo = character.ShipSkill.GetTurretModInfo();
        if (turretInfo != null)
            turretInfo.ChangeTimeReborn(timeReborn);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

