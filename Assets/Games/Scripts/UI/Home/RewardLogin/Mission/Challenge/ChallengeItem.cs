using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeItem : MonoBehaviour, IItem<ChallengeItemData> {
    [SerializeField] private Image icon;
    [SerializeField] private Image progressImage;
    [SerializeField] private Image rewardImage;
    [SerializeField] private Image rankImage;
    [SerializeField] private TextMeshProUGUI nameMission;
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI rewardValue;
    [SerializeField] private TextMeshProUGUI timeNextChallenge;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private ButtonExplorer claimButton;
    [SerializeField] private ButtonExplorer gotoButton;
    [SerializeField] private ButtonExplorer skipButton;
    [SerializeField] private ButtonExplorer skipConfirmButton;
    [SerializeField] private ButtonExplorer skipCancelButton;
    [SerializeField] private ButtonExplorer watchButton;
    [SerializeField] private ButtonExplorer editorCompleteButton;
    [SerializeField] private GameObject activeGroup;
    [SerializeField] private GameObject deactiveGroup;
    [SerializeField] private GameObject skipGroup;
    [SerializeField] private GameObject iconGroup;

    private int index;
    private bool skiped;
    private Action<ChallengeItemData> onClaim;
    private Color normalColor;
    private Color highlightColor;
    private bool isShowRemainText;
    private double timeRemain = 0;
    private double currentTime = 0;
    private TimeSpan timeSpan;
    private Countdowner showTimeRemainTextCD = new Countdowner();
    public ChallengeItemData dataStack { get; set; }

    private void Awake() {
        claimButton.AddEvent(Claim);
        gotoButton.AddEvent(GotoSource);
        watchButton.AddEvent(WatchVideoButton);
        skipButton.AddEvent(SkipButtonOnClick);
        skipConfirmButton.AddEvent(SkipConfirmButtonOnClick);
        skipCancelButton.AddEvent(SkipCancelButtonOnClick);
#if CHEAT
        editorCompleteButton.gameObject.SetActive(true);
        editorCompleteButton.AddEvent(EditorComplete);
#else
        editorCompleteButton.gameObject.SetActive(false);
#endif
#if !UNITY_EDITOR
        watchButton.SetState(Gemmob.Networker.IsInternetAvaiable && EMAdManager.Instance.HasRewardAds());
#endif
    }

    public IItem<ChallengeItemData> Generate() {
        var claimable = dataStack.Claimable;
        activeGroup.SetActive(true);
        deactiveGroup.SetActive(false);
        rewardImage.sprite = dataStack.Rewards[0].Icon;
        rewardValue.text = $"{dataStack.Rewards[0].Amount}";
        rankImage.color = dataStack.Rank.Color;
        rankText.text = dataStack.Rank.Name;
        nameMission.text = dataStack.NameMission;
        targetText.text = $"/{dataStack.PointTarget}";
        progressText.text = $"{dataStack.PointProgress}";
        progressImage.fillAmount = dataStack.GetProgress();
        SetProgressColor(!claimable);
        claimButton.gameObject.SetActive(claimable);
        gotoButton.gameObject.SetActive(!claimable && dataStack.GotoSource != null);
        skipButton.gameObject.SetActive(!claimable);
        isShowRemainText = false;
        return this;
    }
    private void SetStatusUI() {
        activeGroup.SetActive(!skiped);
        skipGroup.SetActive(false);
        deactiveGroup.SetActive(skiped);
        iconGroup.SetActive(!skiped);
        isShowRemainText = skiped;
    }

    private void SetTimeRemain() {
        if (showTimeRemainTextCD.IsTimeOut()) {
            currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            timeRemain = GameResources.Instance.Challenge.TimeReady[index] - currentTime;
            if (timeRemain <= 0) {
                isShowRemainText = false;
                skiped = false;
                PopupHUD.Instance.Mission.Challenge.GenerateOneItem(index, false);
            }
            timeSpan = TimeSpan.FromSeconds(timeRemain);
            timeNextChallenge.text = $"Next challenge in \n {string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds)}";
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
    public ChallengeItem SetNormalColor(Color normalColor) {
        this.normalColor = normalColor;
        return this;
    }
    public ChallengeItem SetHighlighColor(Color highlightColor) {
        this.highlightColor = highlightColor;
        return this;
    }
    public void SetProgressColor(bool isNormal) {
        progressImage.color = isNormal ? normalColor : highlightColor;
    }
    private void SetSkipStatus(bool status) {
        skiped = status;
    }
    public ChallengeItem UpdateUI(ChallengeItemData data, Action<ChallengeItemData> onClaim, int index) {
        dataStack = data;
        this.onClaim = onClaim;
        this.index = index;
        SetSkipStatus(GameResources.Instance.Challenge.IsSkip(index));
        SetStatusUI();
        if (skiped)
            return this;
        Generate();
        return this;
    }
    private void Claim() {
        dataStack.Apply();
        SetSkipStatus(true);
        onClaim?.Invoke(dataStack);
        GameResources.Instance.Challenge.SetTimeReady(index);
        SetStatusUI();
        //PopupHUD.Instance.Mission.Challenge.GenerateOneItem(index, false);
    }
    private void WatchVideoButton() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.skip_challenge, () => {
            SetSkipStatus(false);
            dataStack.ResetData();

            PopupHUD.Instance.Mission.Challenge.GenerateOneItem(index, true);
        });
    }
    private void SkipButtonOnClick() {
        skipGroup.SetActive(true);
    }
    private void SkipConfirmButtonOnClick() {
        SetSkipStatus(true);
        SetStatusUI();
        dataStack.SetOnComplete(true);
        dataStack.Unassign();
        dataStack.ResetProgress();
        GameResources.Instance.Challenge.SetTimeReady(index);
    }
    private void SkipCancelButtonOnClick() {
        skipGroup.SetActive(false);
    }
    private void GotoSource() {
        dataStack.GotoAction();
    }

    private void EditorComplete() {
        dataStack.Upgrade(dataStack.PointTarget);
        Claim();
    }
}
