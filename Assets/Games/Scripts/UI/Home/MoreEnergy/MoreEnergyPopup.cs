using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class MoreEnergyPopup : BasePopup {
    #region Variable
    [SerializeField] private ButtonExplorer buyByGemButton;
    [SerializeField] private ButtonExplorer buyByAds;
    [SerializeField] private TextMeshProUGUI remainText;
    [SerializeField] private TextMeshProUGUI rewardBuyByGemText;
    [SerializeField] private TextMeshProUGUI rewardBuyByAdsText;
    [SerializeField] private TextMeshProUGUI priceByGemText;
    [SerializeField] private LockbarNotify lockbarNotify;
    [Header("Energy UI")]
    [SerializeField] private Transform energyUI;
    [SerializeField] private Image energyIcon;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private float timeLife = 1f;
    private TweenerCore<Vector3, Vector3, VectorOptions> tweenn;
    #endregion

    #region Constructors
    private void Awake() {
        buyByAds.AddEvent(OnBuyByAds);
        buyByGemButton.AddEvent(OnBuyByGem);
    }
    private void OnEnable() {
        UpdateUI();
    }
    private void UpdateUI() {
        var remain = GameResources.Instance.EnergyData.AdsBuy.CurrentRemain;
        remainText.text = $"Remaining today: {remain}";
        rewardBuyByGemText.text = GameResources.Instance.EnergyData.GemBuy.Item.Amount.ToString();
        rewardBuyByAdsText.text = GameResources.Instance.EnergyData.AdsBuy.Item.Amount.ToString();
        priceByGemText.text = GameResources.Instance.EnergyData.GemBuy.Price.Amount.ToString();
        bool hasRemainAds = GameResources.Instance.EnergyData.AdsBuy.HasRemain;
        buyByAds.SetState(EMAdManager.Instance.HasRewardAds() && hasRemainAds);

        //ItemStack needPrice = GameResourcesIG.Instance.EnergyData.GemBuy.Price;
        //ItemStack curItemPrice = GameResourcesIG.Instance.Inventory.GetItem(needPrice.Id);
        //bool enoughPrice = curItemPrice.Amount >= needPrice.Amount;
        //buyByGemButton.SetState(enoughPrice);
        lockbarNotify.gameObject.SetActive(false);
    }
    public void ShowLockBarNotify(Transform trans) {
        lockbarNotify.transform.position = trans.position;
        lockbarNotify.SetOriginPos(trans.position - Vector3.up * 1).SetContent(GameDefine.InsufficientResources, 0.5f).Show();
    }
    #endregion

    #region Function
    private void OnBuyByAds() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.energy_ads, OnBuyByAdsSuccess, UpdateUI);
    }
    private void OnBuyByAdsSuccess() {
        GameResources.Instance.EnergyData.AdsBuy.CurrentRemain--;
        GameResources.Instance.Inventory.Add(GameResources.Instance.EnergyData.AdsBuy.Item);
        GameResources.Instance.DailyMission.AddPointProgress(MissionType.BuyEnergy, 1);
        Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnBuyMoreEnergy);
        OnCloseButtonClicked();
    }
    private void OnBuyByGem() {
        ItemStack needPrice = GameResources.Instance.EnergyData.GemBuy.Price;
        ItemStack curItemPrice = GameResources.Instance.Inventory.GetItem(needPrice.Id);
        bool enoughPrice = curItemPrice.Amount >= needPrice.Amount;
        if (enoughPrice) {
            GameResources.Instance.Inventory.Add(GameResources.Instance.EnergyData.GemBuy.Item);
            GameResources.Instance.Inventory.Add(needPrice.Id, -needPrice.Amount);
            GameResources.Instance.DailyMission.AddPointProgress(MissionType.BuyEnergy, 1);
            Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnBuyMoreEnergy);
            ShowEnergyNeedToPlayUI();
        }
        else {
            ShowLockBarNotify(buyByGemButton.transform);
        }
    }
    public float ShowEnergyNeedToPlayUI() {
        if (energyUI) {
            energyUI.gameObject.SetActive(true);
            energyIcon.SetAlpha(1);
            energyText.SetAlpha(1);
            tweenn?.Kill(true);
            energyIcon.DOKill(true);
            energyText.DOKill(true);
            tweenn = energyUI.DOLocalMoveY(energyUI.transform.localPosition.y + 100, timeLife).SetEase(Ease.Linear)
                .OnStart(() => {
                    energyIcon.DOFade(0, timeLife).SetEase(Ease.Linear);
                    energyText.DOFade(0, timeLife).SetEase(Ease.Linear);
                })
                .OnComplete(() => {
                    energyUI.transform.localPosition = new Vector3(energyUI.transform.localPosition.x, energyUI.transform.localPosition.y - 100, energyUI.transform.localPosition.z);
                    energyUI.gameObject.SetActive(false);
                });
        }
        return timeLife;
    }
    #endregion
}
