using UnityEngine;


[CreateAssetMenu(fileName = "DroneRespawnModData", menuName = "Mod/Buff/Limited/DroneRespawn")]
public class DroneRespawnModData : BuffLimitStackModData {
    [SerializeField] private StatModifier cooldown;
    [SerializeField] private DroneShieldModData droneShieldModData;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        DroneShieldInfo droneShieldInfo = character.ShipSkill.GetDroneModInfo();
        if (droneShieldInfo == null) {
            droneShieldModData.AddDroneModInfo();
            droneShieldInfo = character.ShipSkill.GetDroneModInfo();
            droneShieldInfo.ChangeTimeRespawn(cooldown);
        }
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
    public override bool FirstCondition(ShipBase character) {
        var drone1 = GameResources.Instance.GearInventory.GetDroneLEquipable();
        var drone2 = GameResources.Instance.GearInventory.GetDroneREquipable();
        return drone1 != null || drone2 != null;
    }
}

