using UnityEngine;

[CreateAssetMenu(fileName = "CheckFreePackinShopCondition", menuName = "Resource/Conditions/Shop/CheckFreePackinShopCondition")]
public class CheckFreePackinShopCondition : GameCondition {
    [SerializeField] private ChestItem normalChest;
    [SerializeField] private ChestItem eliteChest;
    [SerializeField] private ShopData shopData;
    [SerializeField] private ChipPackItem chipFree;

    public override bool CheckCondition(object target) {
        return normalChest.IsGetFreeReady() || eliteChest.IsGetFreeReady()
            || shopData.DailyFree.Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year)
            || chipFree.RemainTurn >= chipFree.MaxTurn;
    }
}