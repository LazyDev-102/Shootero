using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmartOfferPopup : DOTweenFrame {
    [SerializeField] private Text originPriceText;
    [SerializeField] private Text realPriceText;
    [SerializeField] private TextMeshProUGUI saleOffText;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private Image rewardFrame;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ButtonExplorer buyButton;

    private SmartOfferData data;
    private SmartOfferInfo pack;
    private void Awake() {
        data = GameResources.Instance.IapPack.SmartOffer;
        closeButton.AddEvent(OnClose);
        buyButton.AddEvent(OnBuy);
    }
    public SmartOfferPopup Initialize() {
        pack = data.GetOfferData();
        UpdateUI();
        return this;
    }
    public override void SpecialTrigger(Action onCompleted) {
        if (!GameResources.Instance.IapPack.SmartOffer.CanSpecialTrigger()) {
            onCompleted?.Invoke();
            return;
        }
        data = GameResources.Instance.IapPack.SmartOffer;
        Initialize();
        var p = PopupHUD.Instance.Show<SmartOfferPopup>();
        p.OnOneShotHide = onCompleted;
    }
    public void UpdateUI() {
        if (pack == null)
        {
            Debug.LogError("OnBuy: No pack initialized! Cant update UI");
            return;
        }
        originPriceText.text = GameIAP.Instance.GetLocalPrice(pack.OriginPrice).localizedPriceString;
        realPriceText.text = GameIAP.Instance.GetLocalPrice(pack.IapKey).localizedPriceString;
        saleOffText.text = $"{pack.SaleOffValue}%";
        rewardIcon.sprite = pack.Reward.Icon;
        rewardFrame.sprite = GameResources.Instance.GearInventory.GetItem(pack.Reward.Id).GearHardData.GetRarety(pack.Rank).Frame;
    }
    private void OnBuy() {
        Debug.Log("===>>> On buy clicked");
        if (pack == null) Initialize(); //hotfix: pack should have been initialized when create popup
        if (pack == null)
        {
            Debug.LogError("OnBuy: No pack initialized! Cant buy");
            return;
        }
        //Tracking.Instance.TrackingIapItemClicked(pack.IapKey);
        GameIAP.Instance.Buy(pack.IapKey, OnBuySuccessed, OnBuyFail);
    }
    private void OnBuySuccessed() {
        //Tracking.Instance.TrackingPurchaseSuccessed(pack.IapKey);
        data.ClaimReward();
        PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(new List<ItemClaim>() { pack.Reward });
        OnClose();
        PanelHUD.Instance.Conqueror.SmartOfferStatus();
    }
    private void OnBuyFail() {
        //Tracking.Instance.TrackingPurchaseFaid(pack.IapKey);
    }
    private void OnClose() {
        Hide();
    }
}
