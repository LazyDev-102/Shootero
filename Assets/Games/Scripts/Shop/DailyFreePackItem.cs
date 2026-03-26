using UnityEngine;
using Helper;

[CreateAssetMenu(fileName = "DailyFreePackItem", menuName = "Resource/Item/Packs/DailyFreePackItem")]
public class DailyFreePackItem : Item {
    [SerializeField] private ItemClaim[] itemClaims;

    public ItemClaim[] ItemClaims { get => itemClaims; }

    public void Assign() {
        RefreshReward();
    }
    private void RefreshReward() {
        if (itemClaims == null || itemClaims.Length == 0)
            return;
        foreach (var item in itemClaims) {
            if (item.Id == ConstantItemID.RandomMatId) {
                var value = (GameResources.Instance.MaterialPerSecond * Constant.HourToSecond).ConvertToInt();
                if (value < 1)
                    value = 1;
                item.Amount = value;

            }
        }
    }
    public override void Claim(int amount) {
        foreach (var item in itemClaims) {
            item.Claim(amount);
        }
    }
}
