using UnityEngine;

[CreateAssetMenu(fileName = "EnhanceShipCondition", menuName = "Resource/Conditions/Ship/Enhance Ship Condition")]
public class EnhanceShipCondition : GameCondition<ShipInfor> {
    public override bool CheckCondition(ShipInfor target) {
        int cLevel = GameResources.Instance.LevelProgress.GetCurrentLevel();
        if (!GameResources.Instance.LevelProgress.Datas.UnlockFeatures.CanUnlockEnhanceShip(cLevel)) {
            return false;
        }
        if (target.Levels.Count <= 0) {
            return false;
        }
        if (target.CurrentLevel >= cLevel)
            return false;
        if (target.IsMax) {
            return false;
        }
        bool canEnhance = target.Unlocked;
        ItemStack price = target.Levels[target.CurrentLevel + 1].Price;
        ItemStack curItem = GameResources.Instance.Inventory.GetItem(price.Id);
        canEnhance &= (curItem.Amount >= price.Amount);
        return canEnhance;
    }
}
