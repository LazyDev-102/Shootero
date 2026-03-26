using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GearItemSelectUpgrade : MonoBehaviour {
    [SerializeField] private GearUpgradeItemItem gearUpgradeItemSelected1;
    [SerializeField] private GearUpgradeItemItem gearUpgradeItemSelected2;
    [SerializeField] private GearUpgradeItemItem gearUpgradeItemSelected3;
    private GearUpgradeItemItem root1;
    private GearUpgradeItemItem root2;
    private GearUpgradeItemItem root3;

    [SerializeField] private Image iconResult;
    [SerializeField] private Image bgResult;
    [SerializeField] private Image tagResult;

    private Action<GearUpgradeItemItem, GearSoftData> onSelect;
    private Sprite currentframe = null;
    private Sprite currentIcon = null;
    private Sprite nextFrame = null;
    private Sprite nextIcon = null;
    private Sprite nextTag = null;
    [HideInInspector] public GearSoftData ItemKey;
    [SerializeField] private DOTweenAnimation moveAnim;

    public void InitData(Action<GearUpgradeItemItem, GearSoftData> onSelect, GearSoftData itemKey) {
        this.onSelect = onSelect;
        this.ItemKey = itemKey;
        gearUpgradeItemSelected2.UpdateSelectButtonStatus(false);
        gearUpgradeItemSelected3.UpdateSelectButtonStatus(false);
        UpdateResultUI(itemKey.GearHardData.Icon, itemKey.GearHardData.GetRarety(itemKey.CurrentRank).Frame);
        UpdateFrameUI(itemKey.GearHardData.GetRarety(itemKey.CurrentRank).Frame);
    }
    private GearSoftData[] gearSoftDatas = new GearSoftData[2];
    public void AddItemUpgrade(GearUpgradeItemItem itemRoot, GearSoftData item) {
        if (gearSoftDatas[0] == null) {
            gearSoftDatas[0] = item;
            root2 = itemRoot;
            var currentRank = gearSoftDatas[0].CurrentRank;
            var hardData = gearSoftDatas[0].GearHardData;
            currentframe = hardData.GetRarety(currentRank).Frame;
            currentIcon = hardData.GetIcon(currentRank);
            //nextFrame = hardData.GetRarety(currentRank + 1).Frame;
            //nextIcon = hardData.Icon;
            //nextTag = hardData.GetRarety(currentRank + 1).TagName;
            gearUpgradeItemSelected2.Initialized(item, onSelect, null);
            gearUpgradeItemSelected2.IsSelect = true;
            gearUpgradeItemSelected2.UpdateIconFrameUI(currentIcon, currentframe);
        }
        else if (gearSoftDatas[1] == null) {
            gearSoftDatas[1] = item;
            root3 = itemRoot;
            gearUpgradeItemSelected3.Initialized(item, onSelect, null);
            gearUpgradeItemSelected3.IsSelect = true;
            gearUpgradeItemSelected3.UpdateIconFrameUI(currentIcon, currentframe);
        }
    }
    private void UpdateResultUI(Sprite icon, Sprite frame) {
        iconResult.sprite = icon;
        bgResult.sprite = frame;
    }
    private void UpdateFrameUI(Sprite sprite) {
        gearUpgradeItemSelected2.UpdateIconFrameUI(null, sprite);
        gearUpgradeItemSelected3.UpdateIconFrameUI(null, sprite);
    }
    public void UpdateRemoveItemUI(int index) {
        switch (index) {
            case 0:
                gearSoftDatas[0] = null;
                gearUpgradeItemSelected2.UpdateIconFrameUI(null, currentframe);
                gearUpgradeItemSelected2.IsSelect = false;
                if (root2 != null)
                    root2.IsSelect = false;
                break;
            case 1:
                gearSoftDatas[1] = null;
                gearUpgradeItemSelected3.UpdateIconFrameUI(null, currentframe);
                gearUpgradeItemSelected3.IsSelect = false;
                if (root3 != null)
                    root3.IsSelect = false;
                break;
            case -1:
                gearSoftDatas[0] = null;
                gearSoftDatas[1] = null;
                gearUpgradeItemSelected3.UpdateIconFrameUI(null, currentframe);
                gearUpgradeItemSelected2.UpdateIconFrameUI(null, currentframe);
                gearUpgradeItemSelected3.IsSelect = false;
                gearUpgradeItemSelected2.IsSelect = false;
                if (root3 != null)
                    root3.IsSelect = false;
                if (root2 != null)
                    root2.IsSelect = false;
                break;
        }
    }
    public void RemoveItem(GearSoftData itemRemove, GearUpgradeItemItem itemGamebject) {
        for (int i = 0; i < gearSoftDatas.Length; i++) {
            if (gearSoftDatas[i] == itemRemove) {
                UpdateRemoveItemUI(i);
                itemGamebject.UpdateSelectButtonStatus(false);
                return;
            }
        }
    }
    public bool CanAddItem() {
        return gearSoftDatas[0] == null || gearSoftDatas[1] == null;
    }
    public bool HasKeyItem() {
        return true;
    }
    private bool equip1;
    private bool equip2;
    public void Upgrade() {
        if (CanAddItem())
            return;
        var gearInv = GameResources.Instance.GearInventory;
        equip1 = gearSoftDatas[0].IsEquiped && gearSoftDatas[0].GearTypeSoft == GearType.Drone1 || gearSoftDatas[1].IsEquiped && gearSoftDatas[1].GearTypeSoft == GearType.Drone1;
        equip2 = gearSoftDatas[0].IsEquiped && gearSoftDatas[0].GearTypeSoft == GearType.Drone2 || gearSoftDatas[1].IsEquiped && gearSoftDatas[1].GearTypeSoft == GearType.Drone2;
        ItemKey.Rankup();
        if (ItemKey.IsDrone) {
            if (equip1)
                gearInv.DroneLSlot.UnEquipItem();
            if (equip2)
                gearInv.DroneRSlot.UnEquipItem();
            if (ItemKey.IsEquiped) {
                if (ItemKey.IsDroneL)
                    gearInv.DroneLSlot.EquipItem(ItemKey);
                else
                    gearInv.DroneRSlot.EquipItem(ItemKey);
            }
            else {
                if (equip1)
                    gearInv.DroneLSlot.UnEquipItem();
                if (equip2)
                    gearInv.DroneRSlot.UnEquipItem();
            }
        }
        else if (gearSoftDatas[0].IsEquiped || gearSoftDatas[1].IsEquiped)
            PanelHUD.Instance.Gear.CurrentGearSlot.UnEquipItem();
        gearInv.Remove(gearSoftDatas[0]);
        gearInv.Remove(gearSoftDatas[1]);
        ResetRootSelect();
    }
    //public void OnDisable() {
    //    transform.localPosition = new Vector3(1080, -282, 0);
    //}
    private void ResetRootSelect() {
        //if(root1 != null) root1.IsSelect = false;
        if (root2 != null)
            root2.IsSelect = false;
        if (root3 != null)
            root3.IsSelect = false;
    }
}