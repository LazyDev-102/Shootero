using UnityEngine;

[CreateAssetMenu(fileName = "AllSeeSelectShipCondition", menuName = "Resource/Conditions/Ship/All See Select Ship Condition")]
public class AllSeeSelectShipCondition : GameCondition {
    [SerializeField] private SeeSelectShipCondition seeShipCondition;
    public override bool CheckCondition(object target) {
        foreach (var s in GameResources.Instance.Ship.Datas) {
            if (seeShipCondition.CheckCondition(s)) {
                return true;
            }
        }
        return false;
    }
}