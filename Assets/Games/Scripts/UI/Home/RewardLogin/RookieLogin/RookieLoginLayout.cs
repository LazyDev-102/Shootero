using System.Collections.Generic;
using UnityEngine;
using GameSystem.Common.UI;
using Gemmob;
using System;

public class RookieLoginLayout : DOTweenFrame, ILayout<RookieLoginItem, RookieLoginInfor> {
    [SerializeField] private Transform container;
    [SerializeField] private RookieLoginItem itemPrefab;
    [SerializeField] private RookieLoginItemSpecial itemSpecial;
    [SerializeField] private ButtonExplorer claimButton;
    [SerializeField] private ButtonExplorer claimX2Button;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ButtonBase tabHideButton;
    [SerializeField] private GameAction onComplete;

    private int currentDay;
    private bool claimable;
    private bool playClaimEffect;
    private List<RookieLoginInfor> rookieInfors;
    public List<RookieLoginItem> Items { get; set; } = new List<RookieLoginItem>();

    #region Init Function
    private void Awake() {
        gameObject.SetActive(!GameResources.Instance.RookieLoginData.IsComplete);
        claimButton.AddEvent(OnClaim);
        claimX2Button?.AddEvent(OnClaimX2);
        closeButton?.AddEvent(OnClose);
        tabHideButton.AddEvent(OnClose);
        SetData();
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
        tabHideButton.SetState(true);
    }
    public override void SpecialTrigger(Action onCompleted) {
        if (!GameResources.Instance.RookieLoginData.CanSpecialTrigger()) {
            onCompleted?.Invoke();
            return;
        }
        var p = PopupHUD.Instance.Show<RookieLoginLayout>();
        p.OnOneShotHide = onCompleted;
    }
    #endregion
    #region Genaral Function
    private void UpdateUI(bool playClaimEffect = false) {
        this.playClaimEffect = playClaimEffect;
        claimable = GameResources.Instance.RookieLoginData.Claimable(DateTime.Now.DayOfYear, DateTime.Now.Year);
        claimButton.SetState(claimable);
        claimX2Button?.SetState(claimable);
        GenerateItem();
    }
    public void GenerateItem() {
        if (Items != null && Items.Count > rookieInfors.Count - 1) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < rookieInfors.Count - 1) {
                    Items[i].Initialized(rookieInfors[i], currentDay, claimable, playClaimEffect);
                }
                Items[i].gameObject.SetActive(i < rookieInfors.Count - 1);
            }
        }
        else {
            for (int i = 0; i < rookieInfors.Count - 1; i++) {
                if (Items == null || i >= Items.Count) {
                    var itemClone = itemPrefab.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    Items.Add(itemClone);
                }
                Items[i].Initialized(rookieInfors[i], currentDay, claimable, playClaimEffect);
                Items[i].gameObject.SetActive(true);
            }
        }
        itemSpecial.Initialized(rookieInfors[rookieInfors.Count - 1], currentDay, claimable, playClaimEffect);
        itemSpecial.gameObject.SetActive(true);
    }
    public void SetData() {
        rookieInfors = GameResources.Instance.RookieLoginData.RookieLoginInfor;
        currentDay = GameResources.Instance.RookieLoginData.CurrentDay;
    }
    private void OnClose() {
        tabHideButton.SetState(false);
        Hide();
        PanelHUD.Instance.Conqueror.RookieLoginStatus();
    }
    private void OnClaim() {
        PopupHUD.Instance.Show<RewardPopup>(hideCurrent: false).UpdateClaimUI(GameResources.Instance.RookieLoginData.GetCurrentReward());
        GameResources.Instance.RookieLoginData.Claim(DateTime.Now.DayOfYear, DateTime.Now.Year);
        SetData();
        UpdateUI(true);
        CheckNotify();
    }
    private void OnClaimX2() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.rookie_ads, () => Hide(), () => {
            GameResources.Instance.RookieLoginData.Claim(DateTime.Now.DayOfYear, DateTime.Now.Year, 2);
            SetData();
            UpdateUI(true);
        });
    }
    private void CheckNotify() {
        PanelHUD.Instance.Conqueror.RookieLoginNotify();
    }
    #endregion
}
