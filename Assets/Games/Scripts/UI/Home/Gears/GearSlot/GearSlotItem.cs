using Gear_Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearSlotItem : MonoBehaviour {
    [SerializeField] private Image iconGear;
    [SerializeField] private Image bgGear;
    [SerializeField] private Image bgTop;
    [SerializeField] private Image bgBottom;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject upgradeableGO;
    [SerializeField] private ButtonExplorer selectButton;

    private GearSlotData data;
    private List<GearSoftData> listItem;
    public GearSlotItem Assign() {
        selectButton.AddEvent(OnSelectGearSlot);
        return this;
    }
    public GearSlotItem UpdateUI(GearSlotData data, List<GearSoftData> listItem) {
        InitData(data, listItem);
        ChangeStatus(data.IsEquiped);
        ChangeContent(data);
        CheckStatusUpgradeable();
        return this;
    }
    private void InitData(GearSlotData data, List<GearSoftData> listItem) {
        this.data = data;
        this.listItem = listItem;
    }
    private void ChangeStatus(bool status) {
        iconGear.gameObject.SetActive(status);
        bgGear.gameObject.SetActive(status);
        bgBottom.SetAlpha(status ? 1 : 0.2f);
        levelText.color = status ? Color.black : Color.white;
    }
    private void ChangeContent(GearSlotData data) {
        levelText.text = $"{data.CurrentLevel + 1}";
        if (data.IsExist) {
            var itemEquip = data.ItemEquip;
            iconGear.sprite = itemEquip.GearHardData.Icon;
            bgGear.sprite = itemEquip.GearHardData.GetRarety(itemEquip.CurrentRank).Frame;
        }
    }
    private void CheckStatusUpgradeable() {
        upgradeableGO.SetActive(data.Enhanceable() && data.EnoughLevel());
    }
    public GearSlotItem SetIconGear(Sprite icon) {
        iconGear.sprite = icon;
        return this;
    }
    public GearSlotItem SetLevel(int level) {
        levelText.text = $"{level}";
        return this;
    }
    public GearSlotItem SetUpgradeable(bool status) {
        upgradeableGO.SetActive(status);
        return this;
    }
    private void OnSelectGearSlot() {
        PopupHUD.Instance.Show<GearSlotItemDetailPopup>().UpdateUI(data, listItem);
    }
}
