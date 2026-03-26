using UnityEngine;

[CreateAssetMenu(fileName = "AllOpenSelectShipCondition", menuName = "Resource/Conditions/Ship/All Open Select Ship Condition")]
public class AllOpenSelectShipCondition : GameCondition {
    [SerializeField] private OpenSelectShipCondition openShipCondition;
    public override bool CheckCondition(object target) {
        foreach (var s in GameResources.Instance.Ship.Datas) {
            if (openShipCondition.CheckCondition(s)) {
                return true;
            }
        }
        return false;
    }
}