using UnityEngine;

[CreateAssetMenu(fileName = "XmasPackItemData", menuName = "Resource/Modes/Xmas/PackItemData")]
public class XmasPackItemData : PackItem {
    [SerializeField] private int buyablePerDay;

    private int buyableRemain;

    public int BuyableRemain { get => buyableRemain; }
    public bool Buyable => buyableRemain > 0;

    public bool Exchangebale => Buyable && GameResources.Instance.Inventory.EnoughPrice(Price);

    private void OnEnable() {
        buyableRemain = buyablePerDay;
    }

    public void LoadData(int buyableCount) {
        buyableRemain = buyableCount;
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
