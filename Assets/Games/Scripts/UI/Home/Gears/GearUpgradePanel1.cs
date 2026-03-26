using GameSystem.Common.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GearUpgradePanel1 : DOTweenFrame {
    #region Variables
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private GearUpgradeItemContainer gearUpgradeItemContainer;

    private bool dataInitialized;
    private bool hasClickUpgrade;
    private List<GearSoftData> data;
    private Action<bool> onClose;
    private GearSoftData gearData;
    #endregion

    #region Init
    private void Awake() {
        backButton.AddEvent(OnClose);
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        if (dataInitialized)
            UpdateUI();
        ToolbarScaler.Instance.SetActive(false);
    }
    #endregion

    #region Function
    public void InitData(Action<bool> onClose, GearSoftData gearData) {
        this.onClose = onClose;
        this.gearData = gearData;
        UpdateUI();
        dataInitialized = true;
    }
    private void UpdateUI() {
        SetStateSelectPanel(false);
    }

    private void SetStateSelectPanel(bool active) {
        GameResources.Instance.GearInventory.SortByRarety();
        data = GameResources.Instance.GearInventory.GearItems.FindAll(x => x.Id == gearData.Id && x.CurrentRank == gearData.CurrentRank && x.IsMaxRank == false && x != gearData);
        gearUpgradeItemContainer.UpdateUI(data, SetStateSelectPanel, gearData, this, SetDataOnUpgradeGear);
    }
    public void OnClose() {
        PopupHUD.Instance.GearDetailItemPopup.InitData(gearUpgradeItemContainer.ItemKey, null, false, false, null);
        Hide();
        ToolbarScaler.Instance.SetActive(true);
        gearUpgradeItemContainer.ReturnStateAllItem();
        onClose?.Invoke(hasClickUpgrade);
        //hasClickUpgrade = false;
    }
    private void SetDataOnUpgradeGear() {
        hasClickUpgrade = true;
    }
    #endregion

    #region Resume
    protected override void OnPause(Action onCompleted = null, bool instant = false) {
        base.OnPause(onCompleted, instant);
        gameObject.SetActive(false);
    }

    protected override void OnResume(Action onCompleted = null, bool instant = false) {
        base.OnResume(onCompleted, instant);
        gameObject.SetActive(true);
        UpdateUI();
    }
    public override Frame OnBack() {
        OnClose();
        return this;
    }
    #endregion
}
