using Gemmob;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyInfor : SingletonFreeAlive<CurrencyInfor> {
    [SerializeField] private ButtonExplorer moreCoinButton;
    [SerializeField] private ButtonExplorer moreGemButton;
    [SerializeField] private ButtonExplorer moreEnergyButton;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private TextMeshProUGUI timeReloadEnergyText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image processImage;
    [SerializeField] private Image bgProcessImage;

    protected override void OnAwake() {
        base.OnAwake();
        moreCoinButton.AddEvent(OpenShopCoin);
        moreGemButton.AddEvent(OpenShopGem);
        moreEnergyButton.AddEvent(OpenShopEnergy);
        //EventDispatcher: GemChange, CoinChang, EnergyChange, ExpChange
        EventDispatcher.Instance.AddListener<EventKey.OnExpChange>(SetProgression);
        EventDispatcher.Instance.AddListener(EventKey.OnChipChanged, OnChipChanged);
        EventDispatcher.Instance.AddListener(EventKey.OnGemChanged, OnGemChanged);
        EventDispatcher.Instance.AddListener(EventKey.OnEnergyChanged, OnEnergyChanged);
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        EventDispatcher.Instance.RemoveListener<EventKey.OnExpChange>(SetProgression);
        EventDispatcher.Instance.RemoveListener(EventKey.OnChipChanged, OnChipChanged);
        EventDispatcher.Instance.RemoveListener(EventKey.OnGemChanged, OnGemChanged);
        EventDispatcher.Instance.RemoveListener(EventKey.OnEnergyChanged, OnEnergyChanged);
    }
    private void OnEnable() {
        UpdateUI();
    }
    private void UpdateUI() {
        SetProgression();
        var inv = GameResources.Instance.Inventory;
        coinText.text = inv.GetItem(ConstantItemID.ChipId).Amount.ToString();
        gemText.text = inv.GetItem(ConstantItemID.GemId).Amount.ToString();
        energyText.text = $"{inv.GetItem(ConstantItemID.EnergyId).Amount}/{GameResources.Instance.EnergyData.GetMaxEnergy()}";
    }
    private void SetProgression() {
        if (!gameObject.activeInHierarchy)
            return;
        var levelData = GameResources.Instance.LevelProgress;
        var maxExp = levelData.Datas.GetMaxExpInLevel();
        var ratio = levelData.GetRatio();
        if (maxExp == -1) {
            processImage.fillAmount = 1;
        }
        else {
            StartCoroutine(Process(ratio));
        }
        levelText.text = $"{levelData.Datas.CurrentLv + 1}";
    }
    private IEnumerator Process(float ratio) {
        var timeUsing = 0f;
        while (timeUsing <= 0.5f) {
            timeUsing += Time.deltaTime;
            processImage.fillAmount = ratio * timeUsing * 2;
            yield return null;
        }
    }
    private void OpenShopCoin() {
        ToolbarScaler.Instance.ShowShopPanel();
        ToolbarScaler.Instance.MoveFrameSelect(0);
    }
    private void OpenShopGem() {
        ToolbarScaler.Instance.ShowShopPanel();
        ToolbarScaler.Instance.MoveFrameSelect(0);
    }
    private void OpenShopEnergy() {
        PopupHUD.Instance.Show<MoreEnergyPopup>();
    }
    private void OnChipChanged() {
        if (coinText)
            coinText.text = GameResources.Instance.Inventory.GetItem(ConstantItemID.ChipId).Amount.ToString();
    }
    private void OnGemChanged() {
        if (gemText)
            gemText.text = GameResources.Instance.Inventory.GetItem(ConstantItemID.GemId).Amount.ToString();
    }
    private void OnEnergyChanged() {
        if (energyText)
            energyText.text = $"{GameResources.Instance.Inventory.GetItem(ConstantItemID.EnergyId).Amount}/{GameResources.Instance.EnergyData.GetMaxEnergy()}";
    }
    public void SetActive(bool active) {
        gameObject.SetActive(active);
    }
}
