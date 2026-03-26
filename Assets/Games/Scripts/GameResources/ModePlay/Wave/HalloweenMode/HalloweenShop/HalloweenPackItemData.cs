using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenPackItemData", menuName = "Resource/Modes/Halloween/PackItemData")]
public class HalloweenPackItemData : PackItem {
    [SerializeField] private int buyablePerDay;

    private int buyableRemain;

    public int BuyableRemain { get => buyableRemain; }
    public bool Buyable => buyableRemain > 0;

    public bool Exchangebale => Buyable && GameResources.Instance.Inventory.EnoughPrice(Price);

    private void OnEnable() {
        buyableRemain = buyablePerDay;
    }

    public void LoadData(int buyableCount, bool fixbug_1_3_20) {
        buyableRemain = buyableCount;
        if(!fixbug_1_3_20) {
            buyableRemain += 54; // 54= 81-27
            if(buyableRemain > 81)
                buyableRemain -= 54;
        }
    }

    public void ResetData() {
        buyableRemain = buyablePerDay;
    }
    public override void Claim(int amount) {
        base.Claim(amount);
        buyableRemain--;
    }

    public string GetRemainExChange() {
        return $"{buyableRemain}/{buyablePerDay}";
    }
}
