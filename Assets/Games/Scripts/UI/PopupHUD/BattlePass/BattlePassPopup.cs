using GameSystem.Common.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Gemmob;
using TMPro;
using DG.Tweening;

public class BattlePassPopup : DOTweenFrame, ILayout<BattlePassItem, BattlePassItemData> {
    [SerializeField] private Transform container;
    [SerializeField] private BattlePassItem itemPrefab;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ButtonExplorer claimAllButton;
    [SerializeField] private ButtonExplorer purchaseButton;
    [SerializeField] private TextMeshProUGUI timeRemain;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI seasonText;
    [SerializeField] private Text priceText;
    [SerializeField] private Text originPriceText;
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private Image progress;
    [SerializeField] private GameObject saleGroup;
    [SerializeField] private GameObject premiumUnlocked;
    [SerializeField] private SpreadEffectUI spreadEffect;
    private BattlePassData data;

    private List<BattlePassItemData> itemDatas;
    private Tween cd;

    public List<BattlePassItem> Items { get; set; } = new List<BattlePassItem>();
    private void Awake() {
        closeButton.AddEvent(OnClose);
        claimAllButton.AddEvent(OnClaimAll);
        purchaseButton.AddEvent(OnPurchase);
        data = GameResources.Instance.BattlePass;
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
        ToolbarScaler.Instance.SetActive(false);
        HeadHUD.Instance.HideAll();
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        //Action newOncompleted = () => {
        //    onCompleted?.Invoke();
        //    ToolbarScaler.Instance.SetActive(true);
        //    HeadHUD.Instance.Show<HeadPanel>();
        //};
        ToolbarScaler.Instance.SetActive(true);
        HeadHUD.Instance.Show<HeadPanel>();
        PanelHUD.Instance.Conqueror.UpdateBattlePassSlider();
        base.OnHide(onCompleted, instant);
        if (cd != null)
            cd.Kill();
    }
    private void UpdateUI() {
        GenerateItem();
        originPriceText.text = GameIAP.Instance.GetLocalPrice(data.OriginIapKey).localizedPriceString;
        priceText.text = GameIAP.Instance.GetLocalPrice(data.PurchaseKey).localizedPriceString;
        claimAllButton.SetState(data.Claimable());
        purchaseButton.gameObject.SetActive(!data.IsPurchase);
        scroll.verticalNormalizedPosition = 1;
        progress.fillAmount = data.Ratio();
        levelText.text = $"{data.Progress}";
        seasonText.text = $"Season {data.SeasonIndex}";
        saleGroup.SetActive(!data.IsPurchase);
        premiumUnlocked.SetActive(data.IsPurchase);
        spreadEffect.UpdateUI(purchaseButton.gameObject.activeInHierarchy);
        UpdateRemainText();
    }
    private void UpdateRemainText() {
        TimeSpan t = TimeSpan.FromSeconds(data.TimeLeft - DateTime.Now.TimeOfDay.TotalSeconds);
        timeRemain.text = $"This season ends in {FormatTime(t)}";
        cd = DOVirtual.DelayedCall(60, () => {
            TimeSpan t1 = TimeSpan.FromSeconds(data.TimeLeft - DateTime.Now.TimeOfDay.TotalSeconds);
            timeRemain.text = $"This season ends in {FormatTime(t1)}";
        }).SetLoops(-1);
    }
    private string FormatTime(TimeSpan timeSpan) {
        if (timeSpan.Days > 0)
            return $"{timeSpan.Days}d{timeSpan.Hours}h";
        if (timeSpan.Hours > 0)
            return $"{timeSpan.Hours}h{timeSpan.Minutes}m";
        if (timeSpan.Minutes > 0)
            return $"{timeSpan.Minutes}m{timeSpan.Seconds}s";
        return $"{timeSpan.Seconds}s";
    }
    public void GenerateItem() {
        itemDatas = data.ItemData;
        if (Items != null && Items.Count > itemDatas.Count) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < itemDatas.Count) {
                    Items[i].Initialized(itemDatas[i], UpdateClaimState);
                }
                Items[i].gameObject.SetActive(i < data.Count);
            }
        }
        else {
            for (int i = 0; i < itemDatas.Count; i++) {
                if (Items == null || i >= Items.Count) {
                    var itemClone = itemPrefab.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    Items.Add(itemClone);
                }
                Items[i].Initialized(itemDatas[i], UpdateClaimState);
                Items[i].gameObject.SetActive(true);
            }
        }
    }
    private void UpdateClaimState() {
        claimAllButton.SetState(data.Claimable());
    }
    private void OnClaimAll() {
        claimAllButton.SetState(false);
        data.ClaimAvailable();
        UpdateUI();
    }
    private void OnPurchase() {
        //Tracking.Instance.TrackingIapItemClicked(data.PurchaseKey);
        GameIAP.Instance.Buy(data.PurchaseKey, OnBuySuccess, OnBuyFailed);
    }
    private void OnBuySuccess() {
        data.SetPurchase(true);
        UpdateUI();
        //Tracking.Instance.TrackingPurchaseSuccessed(data.PurchaseKey);
    }
    private void OnBuyFailed() {
        //Tracking.Instance.TrackingPurchaseFaid(data.PurchaseKey);
    }
    private void OnClose() {
        Hide();
    }
}
