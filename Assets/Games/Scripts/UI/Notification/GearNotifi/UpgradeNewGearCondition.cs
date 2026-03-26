using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeNewGearCondition", menuName = "Resource/Conditions/Gear/Upgrade New Gear Condition")]
public class UpgradeNewGearCondition : GameCondition {
    public override bool CheckCondition(object target) {
        GearInventory gearInventory = GameResources.Instance.GearInventory;
        foreach (var g in gearInventory.GearItems) {
            if (gearInventory.GearHasCombo(g.Id, g.CurrentRank)/* && !g.IsNewChecked*/) {
                return true;
            }
        }
        return false;
    }
}