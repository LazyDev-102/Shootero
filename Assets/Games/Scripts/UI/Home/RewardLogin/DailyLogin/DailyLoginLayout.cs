using System.Collections.Generic;
using UnityEngine;
using GameSystem.Common.UI;
using System;
using Gemmob;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
using GameSystem.Common.Utilities;

public class DailyLoginLayout : DOTweenFrame, ILayout<DailyLoginItem, DailyLoginInfor> {
    #region Variables
    [SerializeField] private Transform container;
    [SerializeField] private DailyLoginItem itemPrefab;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ScrollRect scroll;

    private Tweener tweener;
    private int currentDay;
    private bool claimable;
    private List<DailyLoginInfor> dailyInfors;
    public List<DailyLoginItem> Items { get; set; } = new List<DailyLoginItem>();
    #endregion

    #region Init Function
    private void Awake() {
        SetData();
        closeButton?.AddEvent(OnClose);
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        scroll.verticalNormalizedPosition = 1;
        GenerateItem();
    }
    public override void SpecialTrigger(Action onCompleted) {
        if (!GameResources.Instance.DailyLoginData.CanSpecialTrigger()) {
            onCompleted?.Invoke();
            return;
        }
        var p = PopupHUD.Instance.Show<DailyLoginLayout>();
        p.OnOneShotHide = onCompleted;


    }
    #endregion

    #region Function
    public void GenerateItem() {
        claimable = GameResources.Instance.DailyLoginData.Claimable(DateTime.Now.DayOfYear, DateTime.Now.Year);

        if (Items != null && Items.Count > dailyInfors.Count) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < dailyInfors.Count) {
                    Items[i].Initialized(dailyInfors[i], currentDay, claimable, OnClaimSuccess);
                }
                Items[i].gameObject.SetActive(i < dailyInfors.Count);
            }
        }
        else {
            for (int i = 0; i < dailyInfors.Count; i++) {
                if (Items == null || i >= Items.Count) {
                    var itemClone = itemPrefab.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    Items.Add(itemClone);
                }
                Items[i].Initialized(dailyInfors[i], currentDay, claimable, OnClaimSuccess);
                Items[i].gameObject.SetActive(true);
            }
        }
        DOVirtual.DelayedCall(0.5f, FocusItem);
    }
    private void FocusItem() {
        foreach (var item in Items) {
            if (item.dataStack.Day == currentDay) {
                FocusAt(item.rectTransform());
                break;
            }
        }
    }
    public void SetData() {
        dailyInfors = GameResources.Instance.DailyLoginData.DailyLoginInfor;
        currentDay = GameResources.Instance.DailyLoginData.CurrentDay;
    }
    private void UpdateUI() {
        if (GameResources.Instance.DailyLoginData.IsCompleted) {
            gameObject.SetActive(false);
            return;
        }
        claimable = GameResources.Instance.DailyLoginData.Claimable(DateTime.Now.DayOfYear, DateTime.Now.Year);
        GenerateItem();
    }

    private void OnClose() {
        Hide();
    }
    private void OnClaimSuccess() {
        SetData();
        UpdateUI();
    }
    private void FocusAt(RectTransform rt) {
        float verticalNormalizedPos = scroll.GetVerticalNormalizedPositionAt(rt);
        tweener?.Kill();
        tweener = DOVirtual.Float(scroll.verticalNormalizedPosition,
            verticalNormalizedPos,
            1,
            (value) => {
                scroll.verticalNormalizedPosition = value;
            })
            .SetEase(Ease.InOutCubic);
    }
    #endregion
}
