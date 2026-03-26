
using Gear_Data;
using Gemmob.Tutorial;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearDetailItemPopup : BasePopup {
    #region Refrences
    [Serializable]
    public class GearItemStatView {
        public TextMeshProUGUI BaseValueText;
        public TextMeshProUGUI IncreaseValueText;
    }
    #endregion
    [SerializeField] private float timeScaleText = 0.2f;
    [SerializeField] private Image rankIcon;
    [SerializeField] private Image gearFrame;
    [SerializeField] private Image gearIcon;
    [SerializeField] private Image equipIcon;
    [SerializeField] private Sprite equipSprite;
    [SerializeField] private Sprite unequipSprite;
    [SerializeField] private ButtonExplorer equipButton;
    [SerializeField] private ButtonExplorer fuseButton;
    [SerializeField] private ButtonExplorer chooseButton;
    [SerializeField] private TextMeshProUGUI rankName;
    [SerializeField] private TextMeshProUGUI equipText;
    [SerializeField] private TextMeshProUGUI gearNameText;
    [SerializeField] private TextMeshProUGUI gearDescriptionText;
    [SerializeField] private TextMeshProUGUI titleNextRankText;
    [SerializeField] private Transform container;
    [SerializeField] private ParticleSystem enhanceEffect;
    [SerializeField] private List<GearDetailItemStat> gearDetailItemStats;
    private GearSoftData data;
    private DroneGearHardData convertData;
    private GearItemView itemData;
    private List<RankStat> statsData;
    private List<RankStat> droneStatsData;
    private Action onChoose;
    private bool isPassive;
    private bool isChoose;
    private bool hasEquipOrEnhance;
    private bool actionStatus;
    private void Awake() {
        closeButton.AddEvent(OnClose);
        equipButton.AddEvent(OnEquip);
        fuseButton.AddEvent(OnFuseButtonClick);
        chooseButton.AddEvent(OnChooseButtonClick);
    }
    private void OnEnable() {
        if (!FinishTutorialEquipment()) {
            TutorialSystem.Instance.AssignTarget(TutorialKey.TutorialEquipment, 3, equipButton.gameObject);
        }
    }
    private bool FinishTutorialEquipment() {
        return GameResources.Instance.TutorialSytemData.FinishTutorialEquipment;
    }
    public void InitData(GearSoftData data, GearItemView itemData, bool isPassive, bool isChoose, Action onChoose, bool actionStatus = true) {
        this.data = data;
        this.itemData = itemData;
        this.isPassive = isPassive;
        this.isChoose = isChoose;
        this.onChoose = onChoose;
        this.actionStatus = actionStatus;
        UpdateUI();
    }
    private void UpdateUI() {
        SetActionStatus();
        SetGearSlot(data);
        SetButtonStatus();
        GetStatsData(data.SecondStatIds);
        StatsUpdateUI();
        SetEquipStatus(data.IsEquiped);
        UpdateItemData();
        SetMainItemUI(data.GearHardData, data.CurrentRank);
    }
    private void SetActionStatus() {
        fuseButton.gameObject.SetActive(actionStatus);
        chooseButton.gameObject.SetActive(actionStatus);
    }
    private void SetGearSlot(GearSoftData data) {
        if (!isPassive)
            PanelHUD.Instance.Gear.SetCurrentGearSlot(GameResources.Instance.GearInventory.GetGearSlotByGearType(data.GearTypeSoft));
    }
    private void SetButtonStatus() {
        var hasCombo = GameResources.Instance.GearInventory.GearHasCombo(data.GearHardData.Id, data.CurrentRank);
        equipButton.gameObject.SetActive(!isChoose);
        chooseButton.gameObject.SetActive(isChoose && actionStatus);
        fuseButton.gameObject.SetActive(!isChoose && actionStatus && !isPassive && !data.IsMaxRank && hasCombo);
    }
    private void GetStatsData(List<int> data) {
        if (this.data.IsDrone)
            return;
        statsData = new List<RankStat>();
        for (int i = 0; i < data.Count; i++) {
            statsData.Add(GameResources.Instance.GearData.RankStatData.GetRankStats(data[i]));
        }
    }
    private void StatsUpdateUI() {
        if (gearDetailItemStats == null)
            return;
        GearUpgradeStatsUI(data.IsDrone);
        DroneUpdateStatsUI(data.IsDrone);
    }
    private void GearUpgradeStatsUI(bool isDrone) {
        if (isDrone)
            return;
        for (int i = 0; i < gearDetailItemStats.Count; i++) {
            if (i < statsData.Count)
                gearDetailItemStats[i].UpdateUI(statsData[i], data.CurrentRank, data.IsMaxRank, false);
            else
                gearDetailItemStats[i].UpdateUI(null, -1, false, false);
        }
    }
    private void DroneUpdateStatsUI(bool isDrone) {
        if (!isDrone)
            return;
        convertData = data.GearHardData as DroneGearHardData;
        if (convertData != null) {
            droneStatsData = convertData.SecondStats.RankStat.ToList();
            for (int i = 0; i < gearDetailItemStats.Count; i++) {
                if (i <= data.CurrentRank)
                    gearDetailItemStats[i].UpdateUI(droneStatsData[i], data.CurrentRank, data.IsMaxRank, false);
                else
                    gearDetailItemStats[i].UpdateUI(droneStatsData[i], data.CurrentRank, true, false, 0.2f);
            }
        }
        else {
            for (int i = 0; i < gearDetailItemStats.Count; i++) {
                gearDetailItemStats[i].UpdateUI(null, -1, false, false);
            }
        }
    }
    private void SetEquipStatus(bool status) {
        equipIcon.sprite = status ? equipSprite : unequipSprite;
        equipText.text = status ? "Unequip" : "EQUIP";
    }
    private void UpdateItemData() {
        if (itemData != null)
            itemData.UpdateUI();
    }
    private void SetMainItemUI(GearHardData hardData, int rank) {
        var color = hardData.GetRarety(rank).Color;
        gearNameText.text = hardData.Name;
        gearDescriptionText.text = hardData.Description;
        gearIcon.sprite = hardData.GetIcon(rank);
        gearFrame.sprite = hardData.GetRarety(rank).Frame;
        rankName.text = hardData.GetRarety(rank).TagName;
        rankIcon.SetColor(color);
        gearNameText.SetColor(color);
        titleNextRankText.text = data.IsMaxRank ? "Next rank" : "Max rank";
    }
    private void OnEquip() {
        if (data.IsEquiped) {
            data.SetIsEquiped(false);
            PanelHUD.Instance.Gear.CurrentGearSlot.UnEquipItem();
        }
        else {
            bool oneEquip = GameResources.Instance.GearInventory.DroneRSlot.IsEquiped ^ GameResources.Instance.GearInventory.DroneLSlot.IsEquiped;
            if (!isPassive && data.IsDrone) {
                if (!oneEquip) {
                    EquipDrone();
                    hasEquipOrEnhance = true;
                    return;
                }
                else {
                    EquipDrone(!GameResources.Instance.GearInventory.DroneLSlot.IsEquiped);
                }
            }
            else {
                PanelHUD.Instance.Gear.CurrentGearSlot.UnEquipItem();
                PanelHUD.Instance.Gear.CurrentGearSlot.EquipItem(data);
            }
        }
        UpdateItemData();
        hasEquipOrEnhance = true;
        OnClose();
        if (isPassive) {
            var gearSlot = PanelHUD.Instance.Gear.CurrentGearSlot;
            PopupHUD.Instance.AssignGearActive.Hide();
            PopupHUD.Instance.GearSlotItemDetail.SetStatusMainItem(gearSlot.IsExist);
        }
    }
    private void EquipDrone(bool isDroneL) {
        if (isDroneL) {
            GameResources.Instance.GearInventory.DroneLSlot.UnEquipItem();
            GameResources.Instance.GearInventory.DroneLSlot.EquipItem(data);
            data.SetGearTypeSoft(GearType.Drone1);
        }
        else {
            GameResources.Instance.GearInventory.DroneRSlot.UnEquipItem();
            GameResources.Instance.GearInventory.DroneRSlot.EquipItem(data);
            data.SetGearTypeSoft(GearType.Drone2);
        }
    }
    private void EquipDrone() {
        Hide();
        PanelHUD.Instance.Gear.ChooseEquipDrone(data);
    }
    private void OnFuseButtonClick() {
        Hide();
        PanelHUD.Instance.Show<GearUpgradePanel1>(pauseCurrent: true).InitData(OnFinishUpgrade, data);
    }
    private void OnChooseButtonClick() {
        onChoose?.Invoke();
        Hide();
    }
    private void OnFinishUpgrade(bool hasUpgrade) {
        hasEquipOrEnhance = hasUpgrade;
        Show();
        UpdateUI();
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        if (!actionStatus)
            return;
        PanelHUD.Instance.Gear.OnGearDetailClose(hasEquipOrEnhance);
        hasEquipOrEnhance = false;
        isChoose = false;
    }
    private void OnClose() {
        Hide();
    }
}
