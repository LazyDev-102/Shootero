using UnityEngine;

[CreateAssetMenu(fileName = "XmasShopExchangeable", menuName = "Resource/Conditions/Xmas/XmasShopExchangeable")]
public class XmasShopExchangeable : GameCondition {
    public override bool CheckCondition(object target) {
        return GameResources.Instance.XmasShopData.Exchangeable();
    }
}
