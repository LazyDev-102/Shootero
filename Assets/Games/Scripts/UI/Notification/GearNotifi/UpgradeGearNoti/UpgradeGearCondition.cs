using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeGearCondition", menuName = "Resource/Conditions/Gear/Upgrade Gear Condition")]
public class UpgradeGearCondition : GameCondition {
    public override bool CheckCondition(object target) {
        GearInventory gearInventory = GameResources.Instance.GearInventory;
        foreach (var g in gearInventory.GearItems) {
            if (gearInventory.GearHasCombo(g.Id, g.CurrentRank)) {
                return true;
            }
        }
        return false;
    }
}
