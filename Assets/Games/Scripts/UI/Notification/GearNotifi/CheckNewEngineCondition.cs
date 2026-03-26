using UnityEngine;

[CreateAssetMenu(fileName = "CheckNewEngineCondition", menuName = "Resource/Conditions/Gear/CheckNewEngineCondition")]
public class CheckNewEngineCondition : GameCondition {
    [SerializeField] private NewGearCondition newGearCondition;
    public override bool CheckCondition(object target) {
        var data = GameResources.Instance.GearInventory.GetEngines();
        foreach (var g in data) {
            if (newGearCondition.CheckCondition(g)) {
                return true;
            }
        }
        return false;
    }
}