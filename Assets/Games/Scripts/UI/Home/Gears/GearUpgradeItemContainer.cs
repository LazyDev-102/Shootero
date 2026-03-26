using System;
using System.Collections.Generic;
using UnityEngine;
using Gemmob;

public class GearUpgradeItemContainer : MonoBehaviour, ILayout<GearUpgradeItemItem, GearSoftData> {
    [SerializeField] private GearUpgradeItemItem itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private GearUpgradeItemItem[] itemSelects;
    [SerializeField] private GearItemSelectUpgrade gearItemSelectUpgrade;
    [SerializeField] private ButtonExplorer upgradeButton;
    [SerializeField] private GameObject fuseLEffect;
    [SerializeField] private GameObject fuseREffect;
    public List<GearUpgradeItemItem> Items { get; set; } = new List<GearUpgradeItemItem>();
    private List<GearSoftData> data;
    private Action<bool> onActivePanel;
    private Action onUpgrade;
    private GearUpgradePanel1 gearPanel;

    public GearSoftData ItemKey => gearItemSelectUpgrade.ItemKey;
    private void Awake() {
        upgradeButton.AddEvent(UpgradeClick);
    }
    public void UpdateUI(List<GearSoftData> data, Action<bool> onActivePanel, GearSoftData itemKey, GearUpgradePanel1 gearPanel, Action onUpgrade) {
        this.data = data;
        this.onActivePanel = onActivePanel;
        this.gearPanel = gearPanel;
        this.onUpgrade = onUpgrade;
        SetFuseButtonStatus(false);
        gearItemSelectUpgrade.InitData(OnSelectItem, itemKey);
        GenerateItem();
    }

    public void GenerateItem() {
        if (Items != null && Items.Count > data.Count) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < data.Count) {
                    Items[i].Initialized(data[i], OnSelectItem, HideChoosePanel);
                }
                Items[i].gameObject.SetActive(!Items[i].IsSelect && i < data.Count);
            }
        }
        else {
            for (int i = 0; i < data.Count; i++) {
                if (Items == null || i >= Items.Count) {
                    var skinClone = itemPrefab.Spawn(container);
                    skinClone.transform.localPosition = Vector3.zero;
                    skinClone.transform.localScale = Vector3.one;
                    Items.Add(skinClone);
                }
                Items[i].Initialized(data[i], OnSelectItem, HideChoosePanel);
                Items[i].gameObject.SetActive(!Items[i].IsSelect);
            }
        }
    }

    public void OnSelectItem(GearUpgradeItemItem itemGamebject, GearSoftData itemData) {
        UpdateItemSelect(itemGamebject, itemData);
        bool canAddItem = gearItemSelectUpgrade.CanAddItem();
        bool slots = gearItemSelectUpgrade.HasKeyItem();
        SetFuseButtonStatus(!canAddItem);
        if (canAddItem && slots) {
            foreach (var item in Items) {
                item.SetTick(itemData.GearHardData.Id, itemData.CurrentRank);
            }
        }
        if (!canAddItem) {
            HideChoosePanel(null);
            foreach (var item in Items) {
                item.SetTick(-1, -1, false, false);
            }
        }
    }
    private void UpdateItemSelect(GearUpgradeItemItem itemGamebject, GearSoftData itemData) {
        if (itemGamebject.IsSelect) {
            gearItemSelectUpgrade.AddItemUpgrade(itemGamebject, itemData);
        }
        else {
            gearItemSelectUpgrade.RemoveItem(itemData, itemGamebject);
        }
    }
    public void CheckRemoveAll() {
        bool active = false;
        foreach (var item in itemSelects) {
            if (item.IsSelect) {
                active = true;
                break;
            }
        }
        onActivePanel?.Invoke(active);
    }

    public void ReturnStateAllItem() {
        foreach (var item in Items) {
            item.IsSelect = false;
        }
        gearItemSelectUpgrade.UpdateRemoveItemUI(-1);
    }

    private void UpgradeClick() {
        onUpgrade?.Invoke();
        gearItemSelectUpgrade.Upgrade();
        GameResources.Instance.DailyMission.AddPointProgress(MissionType.FuseGear, 1);
        EventDispatcher.Instance.Dispatch(EventKey.OnFuseGear);
        PanelHUD.Instance.Show<GearUpgradeSuccesPanel>().InitData(this, gearItemSelectUpgrade.ItemKey);
    }
    public void CloseGearPanel() {
        gearPanel.OnClose();
    }
    private void HideChoosePanel(GearUpgradeItemItem itemChoose) {
        foreach (var item in Items) {
            if (item != null && item != itemChoose) {
                item.SetChoosePanelStatus(false);
            }
        }
    }
    private void SetFuseButtonStatus(bool status) {
        upgradeButton.SetState(status);
        fuseLEffect.SetActive(status);
        fuseREffect.SetActive(status);
    }
}
