using UnityEngine;
using UnityEngine.Purchasing;

[System.Serializable]
public class ShipPackInfo {
    [SerializeField] private string packName;
    [SerializeField] private int shipId;
    [SerializeField] private bool bought;
    [SerializeField] private int levelShip;
    [SerializeField] private string iapKey;
    [SerializeField] private string iapKeySale;
    [SerializeField] private float defaulIap;
    [SerializeField] private ProductType productType;
    [SerializeField] private ItemClaim[] rewards;

    public string PackName { get => packName; }
    public int ShipId { get => shipId; }
    public bool Bought { get => bought; }
    public int LevelShip { get => levelShip; }
    public string IAPKey { get => iapKey; }
    public string IapKeySale { get => iapKeySale; }
    public float DefaulIap { get => defaulIap; }
    public ProductType ProductType { get => productType; }
    public ItemClaim[] Rewards { get => rewards; }

    public void RestorePurchase() {
        if (GameIAP.Instance.IsOwned(iapKeySale)) {
            Claim(1);
            SetBought(true);
        }
    }

    public void SetBought(bool status) {
        bought = status;
    }
    public bool Status() {
        return !GameResources.Instance.Ship.GetShipInfor(shipId).Unlocked;
    }
    public bool Claim(int multi) {
        foreach (var item in rewards) {
            if (item.Id > 7000 && item.Id < 7020) {
                item.Claim(levelShip);
            } else
                item.Claim(multi);
        }
        return true;
    }
}
