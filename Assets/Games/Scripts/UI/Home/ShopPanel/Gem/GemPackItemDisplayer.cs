using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Coffee.UIExtensions;

public class GemPackItemDisplayer : View<GemPackItem> {
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtAmount;
    [SerializeField] private Image imgIcon;
    [SerializeField] private GameObject imgX2;
    [SerializeField] private ButtonBase btnBuy;
    [SerializeField] private Text txtPrice;

    private void Start() {
        btnBuy?.AddEvent(OnButtonBuyClicked);
    }

    public override void Show() {
        if (Model == null) {
            return;
        }
        txtName.text = Model.Name;
        txtAmount.text = $"<color=purple>{Model.ItemClaims[0].Amount}</color>" + GetAmountReward(!Model.IsBought);
        imgIcon.sprite = Model.Icon;
        imgX2.SetActive(!Model.IsBought);
        btnBuy.SetState(true);
        txtPrice.text = GameIAP.Instance.GetLocalPrice(Model.IapKey).localizedPriceString;
        TrackingCurrency();
    }

    private void TrackingCurrency() {
        btnBuy.ButtonKey = Tracking.Instance.CompactKey(Model.IapKey);
    }

    private void OnButtonBuyClicked() {
        string key = Model.GetBuyIapKey();
        //Tracking.Instance.TrackingIapItemClicked(key);
        GameIAP.Instance.Buy(key, OnSuccessBuy, OnFailBuy);
    }
    private string GetAmountReward(bool isX2) {
        return isX2 ? $" <color=yellow>+ {Model.ItemClaims[0].Amount}</color>" : "";
    }
    private void OnSuccessBuy() {
        if (!Model.IsBought) {
            Model.IsBought = true;
            foreach (var item in Model.ItemClaims) {
                item.Claim(2);
            }
        }
        else {
            foreach (var item in Model.ItemClaims) {
                item.Claim();
            }
        }
        GameResources.Instance.DailyMission.AddPointProgress(MissionType.PurchaseGemChipPack, 1);
        //Tracking.Instance.TrackingPurchaseSuccessed(Model.GetBuyIapKey());
        Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnPurchaseGemChipPack);
    }

    private void OnFailBuy() {
        //Tracking.Instance.TrackingPurchaseFaid(Model.GetBuyIapKey());
    }
}
