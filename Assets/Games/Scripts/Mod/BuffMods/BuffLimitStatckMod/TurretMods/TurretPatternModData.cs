using UnityEngine;


[CreateAssetMenu(fileName = "TurretPatternModData", menuName = "Mod/Buff/Limited/TurretPatternModData")]
public class TurretPatternModData : BuffLimitStackModData {
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        TurretModInfo turretInfo = character.ShipSkill.GetTurretModInfo();
        if (turretInfo != null)
            turretInfo.ChangePattern(character);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

