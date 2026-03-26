using UnityEngine;


[CreateAssetMenu(fileName = "TurretQuantityModData", menuName = "Mod/Buff/Limited/TurretQuantity")]
public class TurretQuantityModData : BuffLimitStackModData {
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        TurretModInfo turretInfo = character.ShipSkill.GetTurretModInfo();
        if (turretInfo != null)
            turretInfo.ChangeStack();
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

