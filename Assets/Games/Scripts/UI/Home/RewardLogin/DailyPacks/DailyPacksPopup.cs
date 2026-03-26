using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DailyPacksPopup : DOTweenFrame, ILayout<DailyPacksItem, DailyPacksInfo> {
    #region Variables
    [SerializeField] private Transform container;
    [SerializeField] private DailyPacksItem itemPrefab;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ScrollRect scroll;

    private DailyPacksInfo[] dailyInfors;
    public List<DailyPacksItem> Items { get; set; } = new List<DailyPacksItem>();
    #endregion

    #region Init Function
    private void Awake() {
        SetData();
        closeButton?.AddEvent(OnClose);
    }
    #endregion

    #region Function
    public void GenerateItem() {
        if (Items != null && Items.Count > dailyInfors.Length) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < dailyInfors.Length) {
                    Items[i].Initialized(dailyInfors[i]);
                }
                Items[i].gameObject.SetActive(i < dailyInfors.Length);
            }
        }
        else {
            for (int i = 0; i < dailyInfors.Length; i++) {
                if (Items == null || i >= Items.Count) {
                    var itemClone = itemPrefab.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    Items.Add(itemClone);
                }
                Items[i].Initialized(dailyInfors[i]);
                Items[i].gameObject.SetActive(true);
            }
        }
    }
    public void SetData() {
        dailyInfors = GameResources.Instance.DailyPacksData.Packs;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        GenerateItem();
        scroll.verticalNormalizedPosition = 1;
    }
    private void OnClose() {
        Hide();
    }
    #endregion
}
