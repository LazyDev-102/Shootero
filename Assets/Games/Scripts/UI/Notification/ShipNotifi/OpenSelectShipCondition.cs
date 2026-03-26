using UnityEngine;

[CreateAssetMenu(fileName = "OpenSelectShipCondition", menuName = "Resource/Conditions/Ship/Open Select Ship Condition")]
public class OpenSelectShipCondition : GameCondition<ShipInfor> {
    [SerializeField] BuyShipCondition buyCondition;
    [SerializeField] EnhanceShipCondition enhanceCondition;

    public override bool CheckCondition(ShipInfor target) {
        if (buyCondition.CheckCondition(target)) {
            return !target.IsOpenChecked;
        }
        if (enhanceCondition.CheckCondition(target)) {
            return !target.IsOpenChecked;
        }
        target.IsOpenChecked = false;
        return false;
    }
}