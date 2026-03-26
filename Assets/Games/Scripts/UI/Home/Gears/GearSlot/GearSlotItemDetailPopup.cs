using DG.Tweening;
using GameSystem.Common.UI;
using Gear_Data;
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearSlotItemDetailPopup : DOTweenFrame {
    [SerializeField] protected float timeScaleText = 0.2f;
    [SerializeField] protected Image mainIcon;
    [SerializeField] protected Image frameIcon;
    [SerializeField] protected Image materialIcon;
    [SerializeField] protected Image enhanceCurrencyIcon;
    [SerializeField] protected TextMeshProUGUI tileText;
    [SerializeField] protected TextMeshProUGUI levelSlotText;
    [SerializeField] protected TextMeshProUGUI currentlevelText;
    [SerializeField] protected TextMeshProUGUI nextlevelText;
    [SerializeField] protected TextMeshProUGUI currentDescriptionText;
    [SerializeField] protected TextMeshProUGUI nextDescriptionText;
    [SerializeField] protected TextMeshProUGUI enhanceCurrencyNeedText;
    [SerializeField] protected TextMeshProUGUI enhanceNotEnoughLevelText;
    [SerializeField] protected TextMeshProUGUI assignText;
    [SerializeField] protected ButtonExplorer assignButton;
    [SerializeField] protected ButtonExplorer enhanceButton;
    [SerializeField] protected ButtonExplorer closeButton;
    [SerializeField] protected GameObject enhanceNotEnoughLevelGO;
    [SerializeField] protected GameObject enhanceNormalGO;
    [SerializeField] protected GameObject arrow;
    [SerializeField] protected GameObject footerMaxLevel;
    [SerializeField] protected GameObject nextLevelGO;
    [SerializeField] protected GameObject droneNextLevelGO;
    [SerializeField] protected ParticleSystem enhanceEffect;
    [SerializeField] protected GearDetailItemMaterialContainer materialContainer;
    [SerializeField] private LockbarNotify lockbarNotify;
    [Header("Drone")]
    [SerializeField] protected GameObject normalLevelGroup;
    [SerializeField] protected GameObject droneLevelGroup;
    [SerializeField] protected TextMeshProUGUI droneCLevelText;
    [SerializeField] protected TextMeshProUGUI droneNlevelText;
    [SerializeField] protected TextMeshProUGUI droneCPowerText;
    [SerializeField] protected TextMeshProUGUI droneNPowerText;
    [SerializeField] protected TextMeshProUGUI droneCHPText;
    [SerializeField] protected TextMeshProUGUI droneNHPText;

    private ItemStack price;
    private ItemStack matUI;
    private ItemStack matData;
    private GearSlotData data;
    private List<GearSoftData> listItem;
    private bool hasEquipOrEnhance;
    private void Awake() {
        assignButton.AddEvent(OnAssignButtonClick);
        enhanceButton.AddEvent(OnEnhanceButtonClick);
        closeButton.AddEvent(OnCloseButtonClick);
    }
    public void UpdateUI(GearSlotData data, List<GearSoftData> listItem) {
        this.data = data;
        this.listItem = listItem;
        SetGearSlot(data);
        SetStatusMainItem(data.IsExist);
        SetStatusLevelSlot(data);
        SetStatusDroneLevelSlot(data);
        SetStatusMaterial(data);
        SetEnhanceStatus();
    }
    private void SetGearSlot(GearSlotData data) {
        PanelHUD.Instance.Gear.SetCurrentGearSlot(data);
    }
    public void SetStatusMainItem(bool status) {
        tileText.text = data.GearSlotName;
        mainIcon.gameObject.SetActive(status);
        frameIcon.gameObject.SetActive(status);
        assignText.text = status ? "Change" : "Assign";
        if (status) {
            mainIcon.sprite = data.ItemEquip.GearHardData.Icon;
            frameIcon.sprite = data.ItemEquip.GearHardData.GetRarety(data.ItemEquip.CurrentRank).Frame;
        }
    }
    private void SetStatusLevelSlot(GearSlotData data) {
        if (data.IsDroneSlot)
            return;
        normalLevelGroup.SetActive(true);
        droneLevelGroup.SetActive(false);
        var cLevel = data.CurrentLevel;
        var stats = data.Stats[0];
        var isMax = data.IsMaxLevel;
        nextLevelGO.SetActive(!isMax);
        arrow.SetActive(!isMax);
        levelSlotText.text = $"Level {cLevel + 1}";
        currentlevelText.text = levelSlotText.text;
        currentDescriptionText.text = stats.StatData.GetDescription(stats.Values[cLevel].Value);
        if (!isMax) {
            nextlevelText.text = $"Level {cLevel + 2}";
            nextDescriptionText.text = stats.StatData.GetDescription(stats.Values[cLevel + 1].Value);
        }
    }
    private void SetStatusDroneLevelSlot(GearSlotData data) {
        if (!data.IsDroneSlot)
            return;
        normalLevelGroup.SetActive(false);
        droneLevelGroup.SetActive(true);
        var cLevel = data.CurrentLevel;
        var power = data.Stats[1];
        var hp = data.Stats[0];
        var isMax = data.IsMaxLevel;
        droneNextLevelGO.SetActive(!isMax);
        arrow.SetActive(!isMax);
        levelSlotText.text = $"Level {cLevel + 1}";
        droneCLevelText.text = levelSlotText.text;
        droneCPowerText.text = $"D.Power: {power.StatData.GetValueString(power.Values[cLevel].Value)}";
        droneCHPText.text = $"D.HP: {hp.StatData.GetValueString(hp.Values[cLevel].Value)}";
        if (!isMax) {
            droneNlevelText.text = $"Level {cLevel + 2}";
            droneNPowerText.text = $"D.Power: {power.StatData.GetValueString(power.Values[cLevel + 1].Value)}";
            droneNHPText.text = $"D.HP: {hp.StatData.GetValueString(hp.Values[cLevel + 1].Value)}";
        }
    }
    private void SetStatusMaterial(GearSlotData data) {
        var isMax = data.IsMaxLevel;
        materialContainer.gameObject.SetActive(!isMax);
        footerMaxLevel.SetActive(isMax);
        if (!isMax) {
            var lvPlayer = GameResources.Instance.LevelProgress.GetCurrentLevel() + 1;
            var isRequireLevel = data.CurrentLevel >= lvPlayer;
            matUI = data.Levels[data.CurrentLevel].EnhanceRequire[0];
            matData = GameResources.Instance.Inventory.GetItem(matUI.Id);
            if (isRequireLevel) {
                enhanceNotEnoughLevelText.text = $"{lvPlayer + 2}";
                enhanceNotEnoughLevelGO.SetActive(true);
                materialContainer.UpdateUI(matUI.Id, matData.Amount, matUI.Amount);
            }
            else {
                materialContainer.UpdateUI(matUI.Id, matData.Amount, matUI.Amount);
                enhanceCurrencyIcon.sprite = data.Levels[data.CurrentLevel].PriceUpgrade.Icon;
                enhanceCurrencyNeedText.text = $"{data.Levels[data.CurrentLevel].PriceUpgrade.Amount}";
            }
        }
    }
    private void SetEnhanceStatus() {
        lockbarNotify.gameObject.SetActive(false);
        enhanceButton.gameObject.SetActive(!data.IsMaxLevel);
        if (data.IsMaxLevel)
            return;
        price = data.Levels[data.CurrentLevel].PriceUpgrade;
        var levelPlayer = GameResources.Instance.LevelProgress.GetCurrentLevel() + 1;
        var levelItem = data.CurrentLevel;
        var isEnoughLevel = levelPlayer > levelItem;

        enhanceNotEnoughLevelGO.SetActive(!isEnoughLevel);
        enhanceNormalGO.SetActive(isEnoughLevel);
        if (levelPlayer <= levelItem) {
            enhanceNotEnoughLevelText.text = $"{levelItem + 1}";
        }

        enhanceButton.SetState(isEnoughLevel);
    }
    private bool CheckCurrency(bool pay = false) {
        bool enoughCurrency = GameResources.Instance.Inventory.GetItem(price.Id).Amount >= price.Amount;
        bool enoughMaterial = GameResources.Instance.Inventory.GetItem(matData.Id).Amount >= matUI.Amount;
        var result = enoughCurrency && enoughMaterial;
        if (result && pay) {
            GameResources.Instance.Inventory.Add(price.Id, -price.Amount);
            GameResources.Instance.Inventory.Add(matUI.Id, -matUI.Amount);
        }
        if (!enoughMaterial) // Special trigger show ResourcePack
            GameResources.Instance.IapPack.ResourcePack.SetAppear(data);
        return result;
    }
    private void OnAssignButtonClick() {
        PopupHUD.Instance.Show<AssignGearPopup>().UpdateUI(data, listItem);
    }
    private void OnEnhanceButtonClick() {
        if (!CheckCurrency(true)) {
            ShowLockBarNotify(enhanceButton.transform);
            return;
        }
        if (enhanceEffect != null) {
            enhanceEffect.Stop();
            enhanceEffect.Play();
        }
        data.Levelup();
        UpdateUI(data, listItem);
        OnGearLevelChange();
        hasEquipOrEnhance = true;
        //foreach (var item in data.Stats) {
        //    if (data.CurrentLevel != 0)
        //        item.StatData.RemoveStat(item.Values[data.CurrentLevel - 1]);
        //    item.StatData.AddStat(item.Values[data.CurrentLevel]);
        //}
        GameResources.Instance.DailyMission.AddPointProgress(MissionType.UpgradeGearSlot, 1);
        EventDispatcher.Instance.Dispatch(EventKey.OnUpgradeGear);
    }
    public void ShowLockBarNotify(Transform trans) {
        lockbarNotify.transform.position = trans.position;
        lockbarNotify.SetOriginPos(trans.position - Vector3.up * 1).SetContent(GameDefine.InsufficientResources, 0.5f).Show();
    }
    private void OnCloseButtonClick() {
        Hide();
    }
    private void OnGearLevelChange() {
        Vector3 maxScale = new Vector3(1.2f, -1.2f, 1.2f);
        Vector3 minScale = new Vector3(1, -1, 1);
        currentlevelText.transform.DOKill(true);
        currentDescriptionText.transform.DOKill(true);
        nextlevelText.transform.DOKill(true);
        nextDescriptionText.transform.DOKill(true);
        currentlevelText.transform.DOScale(1.2f, timeScaleText).SetEase(Ease.Linear).OnComplete(() => {
            currentlevelText.transform.DOScale(1, timeScaleText).SetEase(Ease.Linear);
        });
        currentDescriptionText.transform.DOScale(maxScale, timeScaleText).SetEase(Ease.Linear).OnComplete(() => {
            currentDescriptionText.transform.DOScale(minScale, timeScaleText).SetEase(Ease.Linear);
        });
        nextlevelText.transform.DOScale(1.2f, timeScaleText).SetEase(Ease.Linear).OnComplete(() => {
            nextlevelText.transform.DOScale(1, timeScaleText).SetEase(Ease.Linear);
        });
        nextDescriptionText.transform.DOScale(maxScale, timeScaleText).SetEase(Ease.Linear).OnComplete(() => {
            nextDescriptionText.transform.DOScale(minScale, timeScaleText).SetEase(Ease.Linear);
        });
    }
    protected override void OnHide(System.Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        PanelHUD.Instance.Gear.OnGearDetailClose(hasEquipOrEnhance);
        if (GameResources.Instance.IapPack.ResourcePack.CanSpecialTrigger()) {
            PopupHUD.Instance.Show<ResourcesPackPopup>();
        }
        hasEquipOrEnhance = false;
    }
}
