using UnityEngine;

[CreateAssetMenu(fileName = "CheckNewShieldCondition", menuName = "Resource/Conditions/Gear/CheckNewShieldCondition")]
public class CheckNewShieldCondition : GameCondition {
    [SerializeField] private NewGearCondition newGearCondition;
    public override bool CheckCondition(object target) {
        var data = GameResources.Instance.GearInventory.GetShields();
        foreach (var g in data) {
            if (newGearCondition.CheckCondition(g)) {
                return true;
            }
        }
        return false;
    }
}