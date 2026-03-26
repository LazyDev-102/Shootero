

using UnityEngine;
using UnityEngine.Purchasing;

[CreateAssetMenu(fileName = "PackItem", menuName = "Resource/Item/Packs/PackItem")]
public class PackItem : Item {
    [SerializeField] private string iapKey;
    [SerializeField] private float defaulIap;
    [SerializeField] private string iapSaleKey;
    [SerializeField] private float defaulSale;
    [SerializeField] private ProductType productType;
    [SerializeField] private string remoteKey;
    [SerializeField] private bool isFake;
    [SerializeField] private float fakeMulti;
    [SerializeField] private bool isSale;
    [SerializeField] private ItemClaim[] itemClaims;

    public string IapKey { get => iapKey; }
    public float DefaulIap { get => defaulIap; }
    public string IapSaleKey { get => iapSaleKey; }
    public float DefaulSale { get => defaulSale; }
    public ProductType ProductType { get => productType; }
    public string RemoteKey { get => remoteKey; }
    public bool IsFake { get => isFake; }
    public float FakeMulti { get => fakeMulti; }
    public bool IsSale { get => isSale; }
    public ItemClaim[] ItemClaims { get => itemClaims; }

    public string GetBuyIapKey() {
        if (IsFake) {
            return IapSaleKey;
        }
        if (IsSale) {
            return IapSaleKey;
        }
        else {
            return IapKey;
        }
    }

    public override void Claim(int amount) {
        foreach (var item in itemClaims) {
            item.Claim();
        }
    }
}
