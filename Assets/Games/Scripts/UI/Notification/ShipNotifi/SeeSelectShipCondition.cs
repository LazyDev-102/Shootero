using UnityEngine;

[CreateAssetMenu(fileName = "SeeSelectShipCondition", menuName = "Resource/Conditions/Ship/See Select Ship Condition")]
public class SeeSelectShipCondition : GameCondition<ShipInfor> {
    [SerializeField] BuyShipCondition buyCondition;
    [SerializeField] EnhanceShipCondition enhanceCondition;

    public override bool CheckCondition(ShipInfor target) {
        if (buyCondition.CheckCondition(target)) {
            return !target.IsSeeChecked;
        }
        if (enhanceCondition.CheckCondition(target)) {
            return !target.IsSeeChecked;
        }
        target.IsSeeChecked = false;
        return false;
    }
}
