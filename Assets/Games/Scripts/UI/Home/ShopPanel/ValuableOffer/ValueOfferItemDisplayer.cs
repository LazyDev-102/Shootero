using UnityEngine;
using TMPro;

public class ValueOfferItemDisplayer : View<PackItem> {
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private ClaimItemCollectionDisplayer rewardCollection;
    [SerializeField] private TextMeshProUGUI txtOldPrice;
    [SerializeField] private TextMeshProUGUI txtSalePrice;
    [SerializeField] private ButtonBase btnBuy;
    [SerializeField] private Transform saleTag;
    [SerializeField] private TextMeshProUGUI txtSaleTagValue;

    private void Start() {
        if (btnBuy) {
            btnBuy.AddEvent(OnButtonBuyClicked);
        }
    }

    public override void Show() {
        if (Model == null) {
            return;
        }
        SetContentName(Model.Name, true);
        ItemClaim[] itemClaims = Model.ItemClaims;
        SetRewardsCollection(itemClaims);
        bool hasSale = Model.IsSale;
        string contentOldPrice = string.Empty;
        string contentPrice = string.Empty;
        contentPrice = GameIAP.Instance.GetLocalPrice(Model.GetBuyIapKey()).localizedPriceString;
        if (hasSale && Model.IsFake) {
            GameIAP.Meta meta = GameIAP.Instance.GetLocalPrice(Model.IapSaleKey);
            contentOldPrice = $"{meta.symbol} {((float)meta.localizedPrice * Model.FakeMulti).ToString("#,##0.##")}";
        }
        else if (hasSale) {
            contentOldPrice = GameIAP.Instance.GetLocalPrice(Model.IapKey).localizedPriceString;
        }
        SetContentOldPrice(contentOldPrice, hasSale);
        SetContentSalePrice(contentPrice, true);
        SetStateButtonBuy(true, true);
        SetStateSaleTag(false);

    }

    public ValueOfferItemDisplayer SetContentName(string content, bool show) {
        if (txtName) {
            txtName.gameObject.SetActive(show);
            if (show) {
                txtName.text = content;
            }
        }
        return this;
    }

    public ValueOfferItemDisplayer SetRewardsCollection(ItemClaim[] items) {
        if (rewardCollection != null && items != null) {
            rewardCollection.SetCapacity(items.Length).SetItems(items).Show();
        }
        return this;
    }

    public ValueOfferItemDisplayer SetContentOldPrice(string content, bool show) {
        if (txtOldPrice) {
            txtOldPrice.gameObject.SetActive(show);
            if (show) {
                txtOldPrice.text = content;
            }
        }
        return this;
    }

    public ValueOfferItemDisplayer SetContentSalePrice(string content, bool show) {
        if (txtSalePrice) {
            txtSalePrice.gameObject.SetActive(show);
            if (show) {
                txtSalePrice.text = content;
            }
        }
        return this;
    }

    public ValueOfferItemDisplayer SetStateButtonBuy(bool interaction, bool show) {
        if (btnBuy) {
            btnBuy.gameObject.SetActive(show);
            if (show) {
                btnBuy.SetState(show);
            }
        }
        return this;
    }

    public ValueOfferItemDisplayer SetStateSaleTag(bool show) {
        if (saleTag) {
            saleTag.gameObject.SetActive(show);
        }
        return this;
    }

    public ValueOfferItemDisplayer SetContentSaleTag(string content, bool show) {
        if (txtSaleTagValue) {
            txtSaleTagValue.gameObject.SetActive(show);
            if (show) {
                txtSaleTagValue.text = content;
            }
        }
        return this;
    }

    private void OnButtonBuyClicked() {
        string key = Model.GetBuyIapKey();
        //Tracking.Instance.TrackingIapItemClicked(key);
        GameIAP.Instance.Buy(key, OnSuccessBuy, OnFailBuy);
    }

    private void OnSuccessBuy() {
        Model.Claim(0);
        //Tracking.Instance.TrackingPurchaseSuccessed(Model.GetBuyIapKey());
    }

    private void OnFailBuy() {
        //Tracking.Instance.TrackingPurchaseFaid(Model.GetBuyIapKey());
    }

}
