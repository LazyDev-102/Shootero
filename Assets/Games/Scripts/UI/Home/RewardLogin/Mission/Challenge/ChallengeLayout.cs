using DG.Tweening;
using Gemmob;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeLayout : MonoBehaviour, ILayout<ChallengeItem, ChallengeItemData> {
    [SerializeField] private TextMeshProUGUI resetText;
    [SerializeField] private TextMeshProUGUI resetOnDeativeText;
    [SerializeField] private TextMeshProUGUI footerText;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor;
    [SerializeField] private int maxChallenge;
    [SerializeField] private List<ChallengeItem> challengeItems;
    [SerializeField] private Image specialRewardImage;
    [SerializeField] private Image specialProgressImage;
    [SerializeField] private TextMeshProUGUI specialRewardText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private GameObject activeGroup;
    [SerializeField] private GameObject deactiveGroup;

    private ChallengeData data;
    private ChallengeItemData[] itemDatas;
    private bool isShowRemainText;
    private double timeRemain = 0;
    private double currentTime = 0;
    private int dayRemain = 0;
    private TimeSpan timeSpan;
    private Countdowner showTimeRemainTextCD = new Countdowner();
    public List<ChallengeItem> Items { get; set; }

    private void Awake() {
        isShowRemainText = true;
    }

    public void Initialize() {
        data = GameResources.Instance.Challenge;
        SetGroupStatus(!data.IsComplete);
        UpdateUITopPrice();
        if (data.IsComplete)
            return;
        //if (itemDatas == null || itemDatas.Length == 0)
        GenerateItem();
        UpdateUI();
    }
    public void UpdateUI() {
        for (int i = 0; i < challengeItems.Count; i++) {
            if (i < itemDatas.Length) {
                challengeItems[i].SetNormalColor(normalColor)
                                 .SetHighlighColor(highlightColor)
                                 .UpdateUI(itemDatas[i], OnClaim, i)
                                 .gameObject.SetActive(true);
            }
        }
        data.CheckClaimSpecialReward();
    }
    private void UpdateUITopPrice() {
        specialRewardImage.sprite = data.SpecialReward[0].Icon;
        specialProgressImage.fillAmount = data.GetProgress();
        specialRewardText.text = $"{data.SpecialReward[0].Amount}";
        progressText.text = $"{data.PointProgress}";
        targetText.text = $"/{data.PointTarget}";
    }
    public void GenerateItem() {
        itemDatas = data.GetFullChallenge(maxChallenge);
    }
    public void GenerateOneItem(int index, bool isAds) {
        itemDatas[index] = data.GetOneChallenge(index, isAds);
        itemDatas[index].ResetData();
        challengeItems[index].SetNormalColor(normalColor)
                             .SetHighlighColor(highlightColor)
                             .UpdateUI(itemDatas[index], OnClaim, index)
                             .gameObject.SetActive(true);
    }
    private void SetTimeRemain() {
        if (showTimeRemainTextCD.IsTimeOut()) {
            dayRemain = data.CheckinDay - DateTime.Now.DayOfYear;
            currentTime = DateTime.Now.TimeOfDay.TotalSeconds;
            timeRemain = Constant.DayToSecond - currentTime;
            if (dayRemain < 0 || timeRemain <= 0)
                timeRemain = 0;
            timeSpan = TimeSpan.FromSeconds(timeRemain);
            resetText.text = $"Reset in {dayRemain}d {timeSpan.Hours}h {timeSpan.Minutes + 1}m";
            resetOnDeativeText.text = $"{dayRemain}d {timeSpan.Hours}h {timeSpan.Minutes + 1}m";
            if (timeRemain <= 0) {
                //isShowRemainText = false;
                GameResources.Instance.Challenge.IsReset(DateTime.Now.DayOfYear, DateTime.Now.Year);
                Initialize();
            }
            showTimeRemainTextCD.StartCountdown(1);
        }
        else {
            showTimeRemainTextCD.Countdowning(Time.deltaTime);
        }
    }
    private void Update() {
        if (isShowRemainText) {
            SetTimeRemain();
        }
    }
    private void OnClaim(ChallengeItemData dataStack) {
        data.AddPointProgress(dataStack.ChallengePoint);
        PopupHUD.Instance.Show<RewardPopup>(hideCurrent: false).UpdateClaimUI(dataStack.Rewards,
            onClose: () => {
                if (data.CheckClaimSpecialReward())
                    Initialize();
            });
        Progress();
        SetGroupStatus(!data.IsComplete);
        PanelHUD.Instance.Conqueror.MissionPopupNotify();
    }

    private void Progress() {
        UpdateUITopPrice();
    }
    private void SetGroupStatus(bool status) {
        activeGroup.SetActive(status);
        deactiveGroup.SetActive(!status);
        footerText.gameObject.SetActive(status);
        resetText.gameObject.SetActive(status);
        resetOnDeativeText.gameObject.SetActive(!status);
    }
}
