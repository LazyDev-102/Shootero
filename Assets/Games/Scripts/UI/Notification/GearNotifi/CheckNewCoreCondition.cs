using UnityEngine;

[CreateAssetMenu(fileName = "CheckNewCoreCondition", menuName = "Resource/Conditions/Gear/CheckNewCoreCondition")]
public class CheckNewCoreCondition : GameCondition {
    [SerializeField] private NewGearCondition newGearCondition;
    public override bool CheckCondition(object target) {
        var data = GameResources.Instance.GearInventory.GetCores();
        foreach (var g in data) {
            if (newGearCondition.CheckCondition(g)) {
                return true;
            }
        }
        return false;
    }
}