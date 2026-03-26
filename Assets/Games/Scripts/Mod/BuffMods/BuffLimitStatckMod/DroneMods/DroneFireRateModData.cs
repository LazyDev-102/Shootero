using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "DroneFireRateModData", menuName = "Mod/Buff/Limited/DroneFireRate")]
public class DroneFireRateModData : BuffLimitStackModData {
    [SerializeField] private StatModifier[] fireRate;
    [SerializeField] private DroneShieldModData droneShieldModData;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        DroneShieldInfo droneShieldInfo = character.ShipSkill.GetDroneModInfo();
        if (droneShieldInfo == null) {
            droneShieldModData.AddDroneModInfo();
            droneShieldInfo = character.ShipSkill.GetDroneModInfo();
        }
        if (droneShieldInfo != null)
            droneShieldInfo.ChangeFireRate(fireRate);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }

    public override bool FirstCondition(ShipBase character) {
        var drone1 = GameResources.Instance.GearInventory.GetDroneLEquipable();
        var drone2 = GameResources.Instance.GearInventory.GetDroneREquipable();
        return drone1 != null || drone2 != null;
    }
}

