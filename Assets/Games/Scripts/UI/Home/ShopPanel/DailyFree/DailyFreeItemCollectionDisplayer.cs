using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Spine.Unity;

public class DailyFreeItemCollectionDisplayer : CollectionDisplayer<DailyFreePackItem> {
    [SerializeField] private DailyFreeItemDisplayer prefab;
    [SerializeField] private Transform layout;
    [SerializeField] private GameObject unlockGroup;
    [SerializeField] private GameObject lockGroup;
    [SerializeField] private ButtonExplorer watchVideoButton;
    [SerializeField] private ButtonExplorer claimButton;
    [SerializeField] private TextMeshProUGUI timeRemainText;
    [SerializeField] private SkeletonGraphic anim;
    [SerializeField] private string animIdle = "Idle";
    [SerializeField] private string animSleep = "Sleep";

    protected readonly List<DailyFreeItemDisplayer> displayers = new List<DailyFreeItemDisplayer>();
    public override int DisplayerCount => displayers.Count;
    protected DailyFreePackInfo data;
    protected ItemClaim[] packs;

    private bool isShowRemainText;
    private double timeRemain = 0;
    private double currentTime = 0;
    private TimeSpan timeSpan;
    private Countdowner showTimeRemainTextCD = new Countdowner();
    public void AssignAwake() {
        watchVideoButton.AddEvent(OnWatchVideo);
        claimButton.AddEvent(OnClaim);
        showTimeRemainTextCD.StartCountdown(0);
        isShowRemainText = data != null && !data.Claimable(DateTime.Now.Day, DateTime.Now.Year);
    }
    public DailyFreeItemCollectionDisplayer SetData(DailyFreePackInfo data) {
        this.data = data;
        packs = new ItemClaim[data.Packs.Length];
        for (int i = 0; i < data.Packs.Length; i++) {
            packs[i] = data.Packs[i].ItemClaims[0];
        }
        return this;
    }
    public DailyFreeItemDisplayer GetDisplayer(int index) {
        if (index < 0 || index >= DisplayerCount) {
            return null;
        }
        return displayers[index];
    }

    public override void Show() {
        for (int i = 0; i < Capacity; i++) {
            if (DisplayerCount == i) {
                displayers.Add(CreateDisplayer());
            }

            DailyFreeItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            DailyFreeItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }
        var claimable = data.Claimable(DateTime.Now.DayOfYear, DateTime.Now.Year);
        SetStatusUnlockGroup(claimable);
        PlayAnim(claimable);
    }
    private void PlayAnim(bool claimable) {
        if (anim == null || anim.AnimationState == null)
            return;
        anim.AnimationState.SetAnimation(0, claimable ? animIdle : animSleep, true);
    }
    public DailyFreeItemDisplayer GetItemView(DailyFreePackItem abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(DailyFreeItemDisplayer displayer, DailyFreePackItem item) {
        if (displayer == null) {
            return;
        }
        displayer.SetModel(item).Show();
    }

    protected DailyFreeItemDisplayer CreateDisplayer() {
        DailyFreeItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }

    private void OnWatchVideo() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.daily_free_pack_x3, () => {
            PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(packs, multi: 3);
            data.Claim(DateTime.Now.DayOfYear, DateTime.Now.Year, 3);
            SetStatusUnlockGroup(false);
        });
    }
    private void OnClaim() {
        PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(packs);
        data.Claim(DateTime.Now.DayOfYear, DateTime.Now.Year, 1);
        SetStatusUnlockGroup(false);
        isShowRemainText = data != null && !data.Claimable(DateTime.Now.Day, DateTime.Now.Year);
    }
    private void SetStatusUnlockGroup(bool status) {
        unlockGroup.SetActive(status);
        lockGroup.SetActive(!status);
    }
    private void SetTimeRemain() {
        if (showTimeRemainTextCD.IsTimeOut()) {
            currentTime = DateTime.Now.TimeOfDay.TotalSeconds;
            timeRemain = Constant.DayToSecond - currentTime;
            timeSpan = TimeSpan.FromSeconds(timeRemain);
            timeRemainText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            if (timeRemain <= 0) {
                isShowRemainText = false;
                SetStatusUnlockGroup(true);
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
}
