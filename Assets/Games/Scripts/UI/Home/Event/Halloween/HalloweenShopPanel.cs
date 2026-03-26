using GameSystem.Common.UI;
using Gemmob;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HalloweenShopPanel : DOTweenFrame, ILayout<HalloweenPackItem, HalloweenPackItemData>
{
    [SerializeField] private HalloweenPackItem itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private TextMeshProUGUI halloweenRewardItemAmountText;

    private HalloweenShopData shopData;
    public List<HalloweenPackItem> Items { get; set; } = new List<HalloweenPackItem>();

    private void Awake() {
        SetData();
        AddEvent();
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnExchangeHalloweenCandy, OnExchangeHalloweenCandy);
    }
    private void AddEvent() {
        closeButton.AddEvent(OnClose);
        EventDispatcher.Instance.AddListener(EventKey.OnExchangeHalloweenCandy, OnExchangeHalloweenCandy);
    }
    private void SetData() {
        if(shopData == null)
            shopData = GameResources.Instance.HalloweenShopData;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
    }

    private void UpdateUI() {
        SetData();
        GenerateItem();
        OnExchangeHalloweenCandy();
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

    private void OnExchangeHalloweenCandy() {
        halloweenRewardItemAmountText.text = $"{GameResources.Instance.Inventory.GetHCandy().Amount}";
    }

    private void OnClose() {
        Hide();
    }
}
