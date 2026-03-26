using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Gemmob;
using System;
using System.Collections;

public class DailyLoginItem : MonoBehaviour, IItem<DailyLoginInfor> {
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private UnityEngine.UI.Image icon;
    [SerializeField] private GameObject notify;
    [SerializeField] private GameObject tick;
    [SerializeField] private GameObject locked;
    [SerializeField] private DailyLoginItemItem dailyLoginItemItem;
    [SerializeField] private Transform container;
    [SerializeField] private ButtonExplorer claimButton;
    [SerializeField] private GameObject timeRemainClaim;
    [SerializeField] private TextMeshProUGUI timeRemainText;
    [SerializeField] private AutoStartShinyEffect effectCursor;

    public DailyLoginInfor dataStack { get; set; }
    private int dayClaimable;
    private bool claimable;
    private ItemClaim[] rewards;
    private List<DailyLoginItemItem> rewardList = new List<DailyLoginItemItem>();

    private Action onClaim;
    private bool isShowRemainText;
    private bool isShowClaimButton;
    private double timeRemain = 0;
    private double currentTime = 0;
    private TimeSpan timeSpan;
    private Countdowner showTimeRemainTextCD = new Countdowner();
    private void Awake() {
        claimButton.AddEvent(OnClaim);
        showTimeRemainTextCD.StartCountdown(0);
    }
    private void Update() {
        if (isShowRemainText) {
            SetTimeRemain();
        }
    }
    public IItem<DailyLoginInfor> Generate() {
        isShowClaimButton = claimable && dataStack.Day == dayClaimable;
        isShowRemainText = dataStack.Day == dayClaimable && !isShowClaimButton;

        dayText.text = $"Day {dataStack.Day}";
        notify.SetActive(isShowClaimButton);
        tick.SetActive(dataStack.Day < dayClaimable);
        locked.SetActive(dataStack.Day < dayClaimable);
        claimButton.gameObject.SetActive(isShowClaimButton);
        timeRemainClaim.SetActive(isShowRemainText);
        PlayEffectCursor(isShowClaimButton);

        if (rewardList != null && rewardList.Count > rewards.Length) {
            for (int i = 0; i < rewardList.Count; i++) {
                if (i < rewards.Length) {
                    rewardList[i].SetData(rewards[i].Amount, rewards[i].Description, rewards[i].Icon);
                }
                rewardList[i].gameObject.SetActive(i < rewards.Length);
            }
        }
        else {
            for (int i = 0; i < rewards.Length; i++) {
                if (rewardList == null || i >= rewardList.Count) {
                    var itemClone = dailyLoginItemItem.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    rewardList.Add(itemClone);
                }
                rewardList[i].SetData(rewards[i].Amount, rewards[i].Description, rewards[i].Icon);
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
            timeRemainText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
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
    public void Initialized(DailyLoginInfor data, int dayClaimable, bool claimable, Action onClaim/*, Action<DailyLoginItem> onClaim*/) {
        this.dataStack = data;
        this.dayClaimable = dayClaimable + 1;
        this.claimable = claimable;
        this.onClaim = onClaim;
        rewards = data.Rewards;
        Generate();
    }
    private void OnClaim() {
        PopupHUD.Instance.Show<RewardPopup>(hideCurrent: false).UpdateClaimUI(GameResources.Instance.DailyLoginData.GetCurrentReward());
        GameResources.Instance.DailyLoginData.Claim(DateTime.Now.DayOfYear, DateTime.Now.Year);
        onClaim?.Invoke();
        CheckNotify();
    }
    private void CheckNotify() {
        PanelHUD.Instance.Conqueror.DailyLoginNotify();
    }
    private void PlayEffectCursor(bool status) {
        if (effectCursor) {
            effectCursor.enabled = status;
        }
    }
}
