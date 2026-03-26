using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AfkPopup : DOTweenFrame {
    [SerializeField] private TextMeshProUGUI chipPerHourText;
    [SerializeField] private TextMeshProUGUI timeRemainText;
    [SerializeField] private ButtonExplorer claimButton;
    [SerializeField] private ButtonExplorer claim2Button;
    [SerializeField] private ButtonExplorer watchButton;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ButtonExplorer tabToHideButton;
    [SerializeField] private GameObject finishGroup;
    [SerializeField] private GameObject unfinishGroup;
    [SerializeField] private RewardItem itemPrefab;
    [SerializeField] private Transform container;

    private AfkData dataStack;
    private ItemClaim[] itemClaim;
    private List<RewardItem> rewardItems = new List<RewardItem>();

    private bool isShowTime;
    private double timeRemain = 0;
    private double currentTime = 0;
    private TimeSpan timeSpan;
    private Countdowner showTimeRemainTextCD = new Countdowner();
    private double cTime { get => DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds; }

    #region Init Function
    private void Awake() {
        claimButton.AddEvent(OnClaimButtonClick);
        claim2Button.AddEvent(OnClaim2ButtonClick);
        watchButton.AddEvent(OnWatchButtonClick);
        closeButton.AddEvent(OnClose);
        tabToHideButton.AddEvent(OnClose);
    }
    private void Update() {
        if (isShowTime) {
            SetTimeRemain();
        }
    }
    public override void SpecialTrigger(Action onCompleted) {
        if (!GameResources.Instance.AFK.CanSpecialTrigger()) {
            onCompleted?.Invoke();
            return;
        }
        var p = PopupHUD.Instance.Show<AfkPopup>();
        p.UpdateUI(GameResources.Instance.AFK);
        p.OnOneShotHide = onCompleted;
    }
    #endregion
    #region Genaral Function
    public void UpdateUI(AfkData data) {
        if (data == null)
            return;
        dataStack = data;
        itemClaim = data.Rewards;
        Generate();
    }
    private void Generate() {
        var isFinish = dataStack.TimeFinishAFK < cTime;
        isShowTime = !isFinish;
        finishGroup.SetActive(isFinish);
        unfinishGroup.SetActive(!isFinish);
        closeButton.gameObject.SetActive(!isFinish);
        tabToHideButton.SetState(true);
        chipPerHourText.text = $"+{Mathf.RoundToInt(GameResources.Instance.ChipPerSecond * Constant.HourToSecond)}/h";
        if (isFinish)
            timeRemainText.text = "AFK Time 04:00:00";
        GenerateReward();
#if !UNITY_EDITOR
        watchButton.SetState(Gemmob.Networker.IsInternetAvaiable);
#endif
    }
    private void GenerateReward() {
        dataStack.RefreshReward();

        if (rewardItems != null && rewardItems.Count > itemClaim.Length) {
            for (int i = 0; i < rewardItems.Count; i++) {
                if (i < itemClaim.Length) {
                    rewardItems[i].UpdateUI(itemClaim[i]);
                }
                rewardItems[i].gameObject.SetActive(itemClaim[i].Amount > 0 && i < itemClaim.Length);
            }
        }
        else {
            for (int i = 0; i < itemClaim.Length; i++) {
                if (rewardItems == null || i >= rewardItems.Count) {
                    var itemClone = itemPrefab.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    rewardItems.Add(itemClone);
                }
                rewardItems[i].UpdateUI(itemClaim[i]);
                rewardItems[i].gameObject.SetActive(itemClaim[i].Amount > 0);
            }
        }
    }
    private void SetTimeRemain() {
        if (showTimeRemainTextCD.IsTimeOut()) {
            timeRemain = dataStack.GetTimeUse();
            timeSpan = TimeSpan.FromSeconds(timeRemain);
            timeRemainText.text = $"AFK Time {string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds)}";
            if (timeRemain >= GameResources.Instance.AFK.TotalTime) {
                isShowTime = false;
            }
            showTimeRemainTextCD.StartCountdown(1);
        }
        else {
            showTimeRemainTextCD.Countdowning(Time.deltaTime);
        }
    }
    private void OnClaimButtonClick() {
        ClaimReward(1);
    }
    private void OnClaim2ButtonClick() {
        ClaimReward(1, true);
    }
    private void OnWatchButtonClick() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.afk_x2, () => {
            ClaimReward(2, true);
        });
    }
    private void ClaimReward(int multi, bool max = false) {
        dataStack.Claim(multi, max);
        PopupHUD.Instance.Show<RewardPopup>(hideCurrent: false).UpdateClaimUI(itemClaim, multi: multi);
        dataStack.ResetData(DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds);
        Generate();
        OnClose();
        PanelHUD.Instance.Conqueror.AfkPopupNotify();
    }
    private void OnClose() {
        Hide();
        tabToHideButton.SetState(false);
    }
    public override Frame OnBack() {
        tabToHideButton.SetState(false);
        return base.OnBack();
    }
    #endregion
}
