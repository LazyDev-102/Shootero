using GameSystem.Common.UI;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;

public class GearUpgradePanel : DOTweenFrame {
    #region Refrences

    #endregion
    #region Variables
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private GameObject selectPanel;
    [SerializeField] private GameObject unSelectPanel;
    [SerializeField] private GearUpgradeItemContainer gearUpgradeItemContainer;
    private bool canUpgrade;
    private List<GearSoftData> data;
    private Action onClose;
    #endregion

    #region Init
    private void Awake() {
        backButton.AddEvent(OnClose);
    }
    #endregion

    #region Function
    public void InitData(Action onClose) {
        this.onClose = onClose;
    }
    private void UpdateUI() {
        SetStateSelectPanel(false);
        //gearUpgradeItemContainer.UpdateUI(GameResourcesIG.Instance.GearInventory.GearItems, SetStateSelectPanel);
    }

    private void SetStateSelectPanel(bool active) {
        selectPanel.SetActive(active);
        unSelectPanel.SetActive(!active);
        GameResources.Instance.GearInventory.SortByRarety();
        data = GameResources.Instance.GearInventory.GearItems.FindAll(x => x.IsMaxRank == false);
        gearUpgradeItemContainer.UpdateUI(data, SetStateSelectPanel, null, null, null);
    }
    private void OnClose() {
        Hide();
        ToolbarScaler.Instance.SetActive(true);
        gearUpgradeItemContainer.ReturnStateAllItem();
        onClose?.Invoke();
    }
    #endregion

    #region Resume
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
        ToolbarScaler.Instance.SetActive(false);
    }
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
