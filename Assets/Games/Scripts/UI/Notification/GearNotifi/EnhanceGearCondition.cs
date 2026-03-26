using UnityEngine;

[CreateAssetMenu(fileName = "EnhanceGearCondition", menuName = "Resource/Conditions/Gear/EnhanceGearCondition")]
public class EnhanceGearCondition : GameCondition {
    public override bool CheckCondition(object target) {
        GearInventory gearInv = GameResources.Instance.GearInventory;
        return gearInv.WeaponrySlot.Enhanceable() ||
               gearInv.ShieldSlot.Enhanceable() ||
               gearInv.CoreSlot.Enhanceable() ||
               gearInv.EngineSlot.Enhanceable() ||
               gearInv.DroneLSlot.Enhanceable() ||
               gearInv.DroneRSlot.Enhanceable();
    }
}