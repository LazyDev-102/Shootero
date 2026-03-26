using UnityEngine;

[CreateAssetMenu(fileName = "BuyShipCondition", menuName = "Resource/Conditions/Ship/Buy Ship Condition")]
public class BuyShipCondition : GameCondition<ShipInfor> {
    public override bool CheckCondition(ShipInfor target) {
        if (target.ComingSoon)
            return false;
        bool canBuy = !target.Unlocked;
        if (target.Levels.Count <= 0) {
            return false;
        }
        int levelProgress = GameResources.Instance.LevelProgress.GetCurrentLevel() + 1;
        if (!target.CanUnlock(levelProgress)) {
            return false;
        }
        ItemStack price = target.Levels[0].Price;
        ItemStack curItem = GameResources.Instance.Inventory.GetItem(price.Id);
        canBuy &= (curItem.Amount >= price.Amount);
        return canBuy;
    }
}
