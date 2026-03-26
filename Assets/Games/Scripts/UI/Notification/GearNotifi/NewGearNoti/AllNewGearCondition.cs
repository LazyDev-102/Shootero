using UnityEngine;

[CreateAssetMenu(fileName = "AllNewGearCondition", menuName = "Resource/Conditions/Gear/All New Gear Condition")]
public class AllNewGearCondition : GameCondition {
    [SerializeField] private NewGearCondition newGearCondition;
    public override bool CheckCondition(object target) {
        foreach (var g in GameResources.Instance.GearInventory.GearItems) {
            if (newGearCondition.CheckCondition(g)) {
                return true;
            }
        }
        return false;
    }
}
