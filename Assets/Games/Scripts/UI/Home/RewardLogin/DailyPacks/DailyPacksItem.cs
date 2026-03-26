using Gemmob;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyPacksItem : MonoBehaviour, IItem<DailyPacksInfo> {
    [SerializeField] private TextMeshProUGUI packName;
    [SerializeField] private Text pricePack;
    [SerializeField] private TextMeshProUGUI timeRemainText;
    [SerializeField] private GameObject lockedBg;
    [SerializeField] private ButtonExplorer claimButton;
    [SerializeField] private ButtonExplorer watchButton;
    [SerializeField] private RewardItem itemChild;
    [SerializeField] private Transform container;

    public DailyPacksInfo dataStack { get; set; }
    private ItemClaim[] rewards;
    private List<RewardItem> rewardList = new List<RewardItem>();

    private bool isShowRemainText;
    private double timeRemain = 0;
    private double currentTime = 0;
    private TimeSpan timeSpan;
    private Countdowner showTimeRemainTextCD = new Countdowner();
    private void Awake() {
        claimButton.AddEvent(OnClaim);
        watchButton.AddEvent(OnWatch);
        showTimeRemainTextCD.StartCountdown(0);
    }
    private void TrackingCurrency() {
        watchButton.ButtonKey = Tracking.Instance.CompactKey(dataStack.IAPKey);
        claimButton.ButtonKey = Tracking.Instance.CompactKey(dataStack.IAPKey);
    }
    private void Update() {
        if (isShowRemainText) {
            SetTimeRemain();
        }
    }
    public IItem<DailyPacksInfo> Generate() {
        isShowRemainText = dataStack.Claimable(DateTime.Now.DayOfYear, DateTime.Now.Year);
        if (rewardList != null && rewardList.Count > rewards.Length) {
            for (int i = 0; i < rewardList.Count; i++) {
                if (i < rewards.Length) {
                    rewardList[i].UpdateUI(rewards[i]);
                }
                rewardList[i].gameObject.SetActive(i < rewards.Length);
            }
        }
        else {
            for (int i = 0; i < rewards.Length; i++) {
                if (rewardList == null || i >= rewardList.Count) {
                    var itemClone = itemChild.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    rewardList.Add(itemClone);
                }
                rewardList[i].UpdateUI(rewards[i]);
                rewardList[i].gameObject.SetActive(true);
            }
        }
        return this;
    }
    private void SetTimeRemain() {
        if (showTimeRemainTextCD.IsTimeOut()) {
            currentTime = DateTime.Now.TimeOfDay.TotalSeconds;
            timeRemain = Constant.DayToSecond - currentTime;
            timeSpan = TimeSpan.FromSeconds(timeRemain);
            timeRemainText.text = dataStack.IsFree ? "Free in " : "Reset in ";
            timeRemainText.text += $"{timeSpan.Hours}h {timeSpan.Minutes}m";
            if (timeRemain <= 0) {
                isShowRemainText = false;
                Generate();
            }
            showTimeRemainTextCD.StartCountdown(1);
        }
        else {
            showTimeRemainTextCD.Countdowning(Time.deltaTime);
        }
    }
    public void Initialized(DailyPacksInfo data) {
        this.dataStack = data;
        rewards = data.Rewards;
        UpdateUI();
        Generate();
        TrackingCurrency();
    }
    private void UpdateUI() {
        packName.text = dataStack.PackName;
        pricePack.text = dataStack.IsFree ? "0.99$" : GameIAP.Instance.GetLocalPrice(dataStack.IAPKey).localizedPriceString;
        SetStatus(dataStack.Claimable(DateTime.Now.DayOfYear, DateTime.Now.Year));
    }
    private void OnClaim() {
        //Tracking.Instance.TrackingIapItemClicked(dataStack.IAPKey);
        GameIAP.Instance.Buy(dataStack.IAPKey, OnSuccessBuy, OnBuyFail);

    }
    private void OnSuccessBuy() {
        var claim = dataStack.Claim(DateTime.Now.DayOfYear, DateTime.Now.Year, 1);
        if (claim) {
            PopupHUD.Instance.Show<RewardPopup>(hideCurrent: false).UpdateClaimUI(dataStack.Rewards).SetTitle(dataStack.IsFree ? "REWARDS" : "YOU'VE GOT");
            SetStatus(dataStack.Claimable(DateTime.Now.DayOfYear, DateTime.Now.Year));
            CheckNotify();
            GameResources.Instance.DailyMission.AddPointProgress(MissionType.PurchaseDailyPack, 1);
        }
        //Tracking.Instance.TrackingPurchaseSuccessed(dataStack.IAPKey);
    }
    private void OnBuyFail() {
        //Tracking.Instance.TrackingPurchaseFaid(dataStack.IAPKey);
    }
    private void OnWatch() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.daily_packs, OnSuccessBuy);
    }
    private void CheckNotify() {
        PanelHUD.Instance.Conqueror.DailyPacksNotify();
    }
    private void SetStatus(bool status) {
        lockedBg.SetActive(!status);
        timeRemainText.gameObject.SetActive(!status);
        claimButton.gameObject.SetActive(status && !dataStack.IsFree);
        watchButton.gameObject.SetActive(status && dataStack.IsFree);
    }
}
