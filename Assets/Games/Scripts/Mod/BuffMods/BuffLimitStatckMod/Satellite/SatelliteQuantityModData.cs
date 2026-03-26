using UnityEngine;


[CreateAssetMenu(fileName = "SatelliteQuantityModData", menuName = "Mod/Buff/Limited/SatelliteQuantity")]
public class SatelliteQuantityModData : BuffLimitStackModData {
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.EnableSatelliteQuantity();
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

