using Gemmob;
using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearEquipment : MonoBehaviour {
    [SerializeField] private GearSlotItem weaponrySlot;
    [SerializeField] private GearSlotItem shieldSlot;
    [SerializeField] private GearSlotItem coreSlot;
    [SerializeField] private GearSlotItem engineSlot;
    [SerializeField] private GearSlotItem droneLSlot;
    [SerializeField] private GearSlotItem droneRSlot;
    [SerializeField] private GearSlotItem droneLSlotEquipment;
    [SerializeField] private GearSlotItem droneRSlotEquipment;
    [SerializeField] private ContainerBase<GearSoftData> view;
    [SerializeField] private Image shipIcon;
    [SerializeField] private Image droneLIcon;
    [SerializeField] private Image droneRIcon;
    [SerializeField] private ParticleSystem[] shipEffects;
    private void Awake() {
        EventDispatcher.Instance.AddListener<EventKey.OnShipChange>(UpdateShipUI);
        shipIcon.sprite = GameResources.Instance.Ship.GetCurrentShip().GetIcon();
        weaponrySlot.Assign();
        shieldSlot.Assign();
        coreSlot.Assign();
        engineSlot.Assign();
        droneLSlot.Assign();
        droneRSlot.Assign();
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnShipChange>(UpdateShipUI);
    }
    private void OnEnable() {
        shipIcon.sprite = GameResources.Instance.Ship.GetCurrentShip().GetIcon();
    }
    public void UpdateUI() {
        LoadWeaponrySlot();
        LoadShieldSlot();
        LoadCoreSlot();
        LoadEngineSlot();
        LoadDroneLSlot();
        LoadDroneRSlot();
    }
    private void UpdateShipUI(EventKey.OnShipChange ship) {
        shipIcon.sprite = GameResources.Instance.Ship.GetShipInfor(ship.shipID).GetIcon();
        foreach (var item in shipEffects) {
            item.ChangeColorParticle(GameResources.Instance.Ship.GetShipInfor(ship.shipID).TrailColor);
        }
    }

    #region New Logic
    private void LoadWeaponrySlot() {
        weaponrySlot.UpdateUI(GameResources.Instance.GearInventory.WeaponrySlot, PanelHUD.Instance.Gear.GearWeaponries);
    }
    private void LoadShieldSlot() {
        shieldSlot.UpdateUI(GameResources.Instance.GearInventory.ShieldSlot, PanelHUD.Instance.Gear.GearShields);
    }
    private void LoadCoreSlot() {
        coreSlot.UpdateUI(GameResources.Instance.GearInventory.CoreSlot, PanelHUD.Instance.Gear.GearCores);
    }
    private void LoadEngineSlot() {
        engineSlot.UpdateUI(GameResources.Instance.GearInventory.EngineSlot, PanelHUD.Instance.Gear.GearEngines);
    }
    private void LoadDroneLSlot() {
        var droneLSlotData = GameResources.Instance.GearInventory.DroneLSlot;
        droneLSlot.UpdateUI(droneLSlotData, PanelHUD.Instance.Gear.GearDrones);
        droneLSlotEquipment.UpdateUI(droneLSlotData, PanelHUD.Instance.Gear.GearDrones);
        if (droneLSlotData.IsExist) {
            droneLIcon.sprite = droneLSlotData.ItemEquip.GearHardData.Icon;
        }
        droneLIcon.gameObject.SetActive(droneLSlotData.IsExist);
    }
    private void LoadDroneRSlot() {
        var droneRSlotData = GameResources.Instance.GearInventory.DroneRSlot;
        droneRSlot.UpdateUI(droneRSlotData, PanelHUD.Instance.Gear.GearDrones);
        droneRSlotEquipment.UpdateUI(droneRSlotData, PanelHUD.Instance.Gear.GearDrones);
        if (droneRSlotData.IsExist) {
            droneRIcon.sprite = droneRSlotData.ItemEquip.GearHardData.Icon;
        }
        droneRIcon.gameObject.SetActive(droneRSlotData.IsExist);
    }
    #endregion
}
