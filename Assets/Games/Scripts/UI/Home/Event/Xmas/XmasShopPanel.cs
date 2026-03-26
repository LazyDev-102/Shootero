using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XmasShopPanel : DOTweenFrame, ILayout<XmasPackItem, XmasPackItemData> {
    [SerializeField] private XmasPackItem itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private TextMeshProUGUI XmasRewardItemAmountText;

    private XmasShopData shopData;
    public List<XmasPackItem> Items { get; set; } = new List<XmasPackItem>();

    private void Awake() {
        SetData();
        AddEvent();
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnExchangeXmasCandy, OnExchangeXmasCandy);
    }
    private void AddEvent() {
        closeButton.AddEvent(OnClose);
        EventDispatcher.Instance.AddListener(EventKey.OnExchangeXmasCandy, OnExchangeXmasCandy);
    }
    private void SetData() {
        if (shopData == null)
            shopData = GameResources.Instance.XmasShopData;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
    }

    private void UpdateUI() {
        SetData();
        GenerateItem();
        OnExchangeXmasCandy();
    }
    public void GenerateItem() {
        for (int i = 0; i < shopData.Packs.Length; i++) {
            if (i >= Items.Count) {
                var itemClone = itemPrefab.Spawn(container);
                itemClone.transform.localPosition = Vector3.zero;
                itemClone.transform.localScale = Vector3.one;
                Items.Add(itemClone);
            }
            Items[i].Initialize(shopData.Packs[i]);
            Items[i].gameObject.SetActive(true);
        }
    }

    private void OnExchangeXmasCandy() {
        XmasRewardItemAmountText.text = $"{GameResources.Instance.Inventory.GetXCandy().Amount}";
    }

    private void OnClose() {
        Hide();
    }
}
