using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPackPopup : DOTweenFrame {
    [SerializeField] private TextMeshProUGUI timeLimitText;
    [SerializeField] private Text originPriceText;
    [SerializeField] private Text realPriceText;
    [SerializeField] private TextMeshProUGUI saleOffText;
    [SerializeField] private List<RewardItem> rewards;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ButtonExplorer buyButton;

    private List<ItemClaim> rewardData;
    private ResourcePackData data;
    private void Awake() {
        data = GameResources.Instance.IapPack.ResourcePack;
        rewardData = data.Rewards;
        closeButton.AddEvent(OnClose);
        buyButton.AddEvent(OnBuy);
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
    }
    public override void SpecialTrigger(Action onCompleted) {
        if (!GameResources.Instance.IapPack.ResourcePack.CanSpecialTrigger()) {
            onCompleted?.Invoke();
            return;
        }
        var p = PopupHUD.Instance.Show<ResourcesPackPopup>();
        p.OnOneShotHide = onCompleted;
    }
    public void UpdateUI() {
        if (rewardData == null)
            return;
        timeLimitText.text = $"Resource Pack";
        originPriceText.text = GameIAP.Instance.GetLocalPrice(data.OriginPrice).localizedPriceString;
        realPriceText.text = GameIAP.Instance.GetLocalPrice(data.IapKey).localizedPriceString;
        saleOffText.text = $"{data.SaleOffValue}%";
        Generate();
    }
    private void Generate() {
        for (int i = 0; i < rewards.Count; i++) {
            rewards[i].UpdateUI(rewardData[i]);
        }
    }
    private void OnBuy() {
        //Tracking.Instance.TrackingIapItemClicked(data.IapKey);
        GameIAP.Instance.Buy(data.IapKey, OnBuySuccessed, OnBuyFail);
    }
    private void OnBuySuccessed() {
        data.ClaimReward();
        PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(data.Rewards);
        //Tracking.Instance.TrackingPurchaseSuccessed(data.IapKey);
    }
    private void OnBuyFail() {
        //Tracking.Instance.TrackingPurchaseFaid(data.IapKey);
    }
    private void OnClose() {
        Hide();
    }
}
