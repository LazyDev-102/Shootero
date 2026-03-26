
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardAdsItemDisplayer : View<ShopRewardAdsPackItem> {
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI nextWatchText;
    [SerializeField] private TextMeshProUGUI txtAmount;
    [SerializeField] private Image icon;
    [SerializeField] private Image progress;
    [SerializeField] private ButtonExplorer watchButton;
    [SerializeField] private GameObject lockThis;
    [SerializeField] private GameObject progressBg;

    Action onClaimed;

    private void Start() {
        watchButton.AddEvent(OnWatchVideo);
    }

    public override void Show() {
        if (Model == null) {
            return;
        }
        UpdateUI();
    }
    private void OnWatchVideo() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.reroll_ads, () => {
            Model.Claim(1);
            UpdateUI();
            onClaimed?.Invoke();
        });
    }
    public void AddActionOnClaim(Action onClaimed) {
        this.onClaimed = onClaimed;
    }
    private void UpdateUI() {
        progressText.text = $"{Model.CTurn}/{Model.MaxTurn}";
        nextWatchText.text = $"Watch {Model.CTurn + 1}/{Model.MaxTurn}";
        txtAmount.text = $"x{Model.ItemClaims.Amount}";
        icon.sprite = Model.Icon;
        progress.fillAmount = Model.Ratio();
        watchButton.gameObject.SetActive(Model.CanWatch);
        progressBg.gameObject.SetActive(Model.CanWatch);
        lockThis.SetActive(!Model.CanWatch);
    }
}
