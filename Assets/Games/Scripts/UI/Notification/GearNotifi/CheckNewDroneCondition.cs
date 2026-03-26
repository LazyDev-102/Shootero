using UnityEngine;

[CreateAssetMenu(fileName = "CheckNewDroneCondition", menuName = "Resource/Conditions/Gear/CheckNewDroneCondition")]
public class CheckNewDroneCondition : GameCondition {
    [SerializeField] private NewGearCondition newGearCondition;
    public override bool CheckCondition(object target) {
        var data = GameResources.Instance.GearInventory.GetDrones();
        foreach (var g in data) {
            if (newGearCondition.CheckCondition(g)) {
                return true;
            }
        }
        return false;
    }
}