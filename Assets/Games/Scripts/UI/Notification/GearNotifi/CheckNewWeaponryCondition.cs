using UnityEngine;

[CreateAssetMenu(fileName = "CheckNewWeaponryCondition", menuName = "Resource/Conditions/Gear/CheckNewWeaponryCondition")]
public class CheckNewWeaponryCondition : GameCondition {
    [SerializeField] private NewGearCondition newGearCondition;
    public override bool CheckCondition(object target) {
        var data = GameResources.Instance.GearInventory.GetWeaponries();
        foreach (var g in data) {
            if (newGearCondition.CheckCondition(g)) {
                return true;
            }
        }
        return false;
    }
}