

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardAdsItemCollectionDisplayer : CollectionDisplayer<ShopRewardAdsPackItem> {
    [SerializeField] private RewardAdsItemDisplayer prefab;
    [SerializeField] private Transform layout;
    [SerializeField] private RewardAdsPogressItemDisplayer[] progress;
    [SerializeField] private Image fillImage;

    protected readonly List<RewardAdsItemDisplayer> displayers = new List<RewardAdsItemDisplayer>();
    public override int DisplayerCount => displayers.Count;

    public RewardAdsItemDisplayer GetDisplayer(int index) {
        if (index < 0 || index >= DisplayerCount) {
            return null;
        }
        return displayers[index];
    }

    public void UpdateProgress(float value) {
        fillImage.fillAmount = value;
    }

    private void OnClaimedRewardAds() {
        UpdateProgress(GameResources.Instance.ShopData.RewardAds.Ratio());
        for (int i = 0; i < progress.Length; i++) {
            progress[i].UpdateUI();
        }
    }

    public override void Show() {
        for (int i = 0; i < Capacity; i++) {
            if (DisplayerCount == i) {
                displayers.Add(CreateDisplayer());
            }

            RewardAdsItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(true);
                SetupDisplayer(displayer, GetItem(i));
            }
        }

        for (int i = Capacity; i < DisplayerCount; i++) {
            RewardAdsItemDisplayer displayer = GetDisplayer(i);
            if (displayer) {
                displayer.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < progress.Length; i++) {
            progress[i].UpdateUI();
        }
    }

    public RewardAdsItemDisplayer GetItemView(ShopRewardAdsPackItem abilityData) {
        foreach (var displayer in displayers) {
            if (displayer.Model == abilityData) {
                return displayer;
            }
        }
        return null;
    }

    public void SetupDisplayer(RewardAdsItemDisplayer displayer, ShopRewardAdsPackItem item) {
        if (displayer == null) {
            return;
        }
        displayer.AddActionOnClaim(OnClaimedRewardAds);
        displayer.SetModel(item).Show();
    }

    protected RewardAdsItemDisplayer CreateDisplayer() {
        RewardAdsItemDisplayer viewItem = Instantiate(prefab, layout);
        return viewItem;
    }
}
