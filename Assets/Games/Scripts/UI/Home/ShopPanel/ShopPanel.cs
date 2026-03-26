using UnityEngine;
using GameSystem.Common.UI;
using System;
using UnityEngine.UI;
using GameSystem.Common.Utilities;
using DG.Tweening;
using System.Collections.Generic;
using Gemmob;

public class ShopPanel : DOTweenFrame {
    [SerializeField] private ValueOfferCollectionDisplayer valueOfferCollectionDislayer;
    [SerializeField] private ChestItemCollectionDisplayer chestCollection;
    [SerializeField] private GemPackItemCollectionDisplayer gemPackItemCollectionDisplayer;
    [SerializeField] private ChipPackItemCollectionDisplayer chipPackItemCollectionDisplayer;
    [SerializeField] private DailyFreeItemCollectionDisplayer dailyFreeItemCollectionDisplayer;
    [SerializeField] private RerollPackItemCollectionDisplayer rerollPackItemCollectionDisplayer;
    [SerializeField] private RewardAdsItemCollectionDisplayer rewardAdsItemCollectionDisplayer;
    [SerializeField] private SkillsPackDisplay skillPack;
    [SerializeField] private ButtonBase btnOpenChests;
    [SerializeField] private ItemView priceChestsView;
    [SerializeField] private ItemView oldPriceChestsView;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private LockbarNotify lockbar;

    private Tween focusTween;

    private void Start() {
        btnOpenChests?.AddEvent(OnOpenChestsButtonClicked);
        dailyFreeItemCollectionDisplayer.AssignAwake();
    }

    private void OnEnable() {
        EventDispatcher.Instance.AddListener(EventKey.OnInventoryChanged, OnInventoryChange);
        EventDispatcher.Instance.AddListener<EventKey.OnOpenChest>(ReloadChest);

    }

    private void OnDisable() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnInventoryChanged, OnInventoryChange);
        EventDispatcher.Instance.RemoveListener<EventKey.OnOpenChest>(ReloadChest);

    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        LoadData();
        skillPack.SetActive();
        scrollRect.verticalNormalizedPosition = 1;
        lockbar.gameObject.SetActive(false);
        var tut = GameResources.Instance.TutorialSytemData;
        scrollRect.enabled = tut.FinishTutorialOpenChest;
        if (tut.IsOpenSkillTutorial && skillPack.gameObject.activeInHierarchy) {
            FocusSkill();
            scrollRect.enabled = false;
        }
    }

    private void LoadData() {
        GameResources.Instance.ShopData.ReloadData();
        SetValueOfferCollection();
        SetGemPackItemCollection();
        SetChipPackItemCollection();
        SetDailyFreePackCollectionCollection();
        SetRerollPackItemCollection();
        SetShopRewardAdsPackItemCollection();
        SetChestCollection();
        SetOpenChests();
    }

    private void ReloadChest() {
        SetChestCollection();
        SetOpenChests();
    }

    private void OnInventoryChange() {
        if (PanelHUD.Instance.GetFrameOnTop() == this) {
            LoadData();
        }
    }

    private void SetChestCollection() {
        if (chestCollection) {
            ChestItem[] chests = GameResources.Instance.ShopData.GetAllChest();
            chestCollection.SetCapacity(chests.Length).SetItems(chests).Show();
        }
    }

    private void SetValueOfferCollection() {
        if (valueOfferCollectionDislayer) {
            PackItem[] packs = GameResources.Instance.ShopData.Packs;
            valueOfferCollectionDislayer.SetCapacity(packs.Length).SetItems(packs).Show();
        }
    }

    private void SetDailyFreePackCollectionCollection() {
        if (dailyFreeItemCollectionDisplayer) {
            DailyFreePackItem[] dailyFreePack = GameResources.Instance.ShopData.DailyFree.Packs;
            dailyFreeItemCollectionDisplayer.SetData(GameResources.Instance.ShopData.DailyFree)
                                            .SetCapacity(dailyFreePack.Length).SetItems(dailyFreePack).Show();
        }
    }
    private void SetGemPackItemCollection() {
        if (gemPackItemCollectionDisplayer) {
            GemPackItem[] gems = GameResources.Instance.ShopData.Gems;
            gemPackItemCollectionDisplayer.SetCapacity(gems.Length).SetItems(gems).Show();
        }
    }

    private void SetChipPackItemCollection() {
        if (chipPackItemCollectionDisplayer) {
            ChipPackItem[] chips = GameResources.Instance.ShopData.Chips.Packs;
            chipPackItemCollectionDisplayer.SetCapacity(chips.Length).SetItems(chips).Show();
        }
    }
    private void SetRerollPackItemCollection() {
        if (rerollPackItemCollectionDisplayer) {
            RerollPackItem[] chips = GameResources.Instance.ShopData.Rerolls.Packs;
            rerollPackItemCollectionDisplayer.SetCapacity(chips.Length).SetItems(chips).Show();
        }
    }
    private void SetShopRewardAdsPackItemCollection() {
        if (rewardAdsItemCollectionDisplayer) {
            var data = GameResources.Instance.ShopData.RewardAds;
            ShopRewardAdsPackItem[] chips = data.Packs;
            rewardAdsItemCollectionDisplayer.UpdateProgress(data.Ratio());
            rewardAdsItemCollectionDisplayer.SetCapacity(chips.Length).SetItems(chips).Show();
        }
    }

    private void OnOpenChestsButtonClicked() {
        ShopData shop = GameResources.Instance.ShopData;
        ItemStack price = shop.PriceChests;
        ItemStack curPrice = GameResources.Instance.Inventory.GetItem(price.Id);
        bool canBuy = curPrice.Amount >= price.Amount;
        if (!canBuy) {
            ShowLockBarNotify(btnOpenChests.transform);
            return;
        }
        ChestItem eliteChest = GameResources.Instance.ShopData.EliteChest;
        GameResources.Instance.Inventory.Remove(price);
        List<GearSoftData> items = new List<GearSoftData>();
        for (int i = 0; i < GameResources.Instance.ShopData.NumberOpenChests; ++i) {
            items.Add(eliteChest.OpenChest());
        }
        PopupHUD.Instance.OpenChest.SetGears(items)
                                   .SetChest(eliteChest);
        PopupHUD.Instance.Show<OpenChestPopup>();
        Tracking.Instance.LogShop(ShopButton.chest_elite_10);
    }

    private void SetOpenChests() {
        ShopData shop = GameResources.Instance.ShopData;
        ItemStack price = shop.PriceChests;
        ItemStack oldPrice = shop.OldPriceChests;
        oldPriceChestsView.SetModel(oldPrice).Show();
        priceChestsView.SetModel(price).Show();
    }

    public void FocusValueOffer() {
        FocusAt(valueOfferCollectionDislayer.transform.parent.rectTransform());
    }

    public void FocusChest() {
        FocusAt(chestCollection.transform.parent.rectTransform());
    }
    public void FocusSkill() {
        FocusAt(skillPack.transform.rectTransform());
    }

    public void FocusGem() {
        FocusAt(gemPackItemCollectionDisplayer.transform.parent.rectTransform());
    }

    public void FocusChip() {
        FocusAt(chipPackItemCollectionDisplayer.transform.parent.rectTransform());
    }
    public void FocusDailyFreePack() {
        FocusAt(dailyFreeItemCollectionDisplayer.transform.parent.rectTransform());
    }
    private void FocusAt(RectTransform rt) {
        float verticalNormalizedPos = scrollRect.GetVerticalNormalizedPositionAt(rt);
        focusTween?.Kill();
        focusTween = DOVirtual.Float(scrollRect.verticalNormalizedPosition,
            verticalNormalizedPos,
            1,
            (value) => {
                scrollRect.verticalNormalizedPosition = value;
            })
            .SetEase(Ease.InOutCubic);
    }
    public void ShowLockBarNotify(Transform trans) {
        lockbar.transform.position = trans.position;
        lockbar.SetOriginPos(trans.position - Vector3.up * 1).SetContent(GameDefine.InsufficientResources, 0.5f).Show();
    }
}
