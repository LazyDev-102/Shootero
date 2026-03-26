using UnityEngine;

[CreateAssetMenu(fileName = "XmasShopExchangeable", menuName = "Resource/Conditions/Xmas/XmasShopExchangeable")]
public class HalloweenShopExchangeable : GameCondition {
    public override bool CheckCondition(object target) {
        return GameResources.Instance.HalloweenShopData.Exchangeable();
    }
}
