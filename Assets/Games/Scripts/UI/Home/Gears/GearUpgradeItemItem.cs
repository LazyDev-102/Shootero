
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearUpgradeItemItem : MonoBehaviour, IItem<GearSoftData> {
    [SerializeField] private Image icon;
    [SerializeField] private Image frame;
    [SerializeField] private GameObject notice;
    [SerializeField] private GameObject tick;
    [SerializeField] private Image locked;
    [SerializeField] private GameObject equipmentGO;
    [SerializeField] private ButtonExplorer selectButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject choosePanel;
    [SerializeField] private ButtonExplorer chooseButton;
    [SerializeField] private ButtonExplorer infoButton;
    public bool IsSelect;
    public GearSoftData dataStack { get; set; }
    private Action<GearUpgradeItemItem, GearSoftData> onSelect;
    private Action<GearUpgradeItemItem> onFocus;

    private void Awake() {
        selectButton?.AddEvent(OnSelectItem);
        chooseButton?.AddEvent(OnChooseButtonClick);
        infoButton?.AddEvent(OnInfoButtonClick);
    }

    public IItem<GearSoftData> Initialized(GearSoftData data, Action<GearUpgradeItemItem, GearSoftData> onSelect, Action<GearUpgradeItemItem> onFocus) {
        this.dataStack = data;
        this.onSelect = onSelect;
        this.onFocus = onFocus;
        return Generate();
    }

    public void SetChoosePanelStatus(bool status, bool auto = false) {
        if (choosePanel == null)
            return;
        if (auto) {
            choosePanel.SetActive(!choosePanel.activeInHierarchy);
        }
        else {
            choosePanel.SetActive(status);
            //UpdateSelectButtonStatus(!status);
        }
    }
    private void OnChooseButtonClick() {
        IsSelect = !IsSelect;
        onSelect?.Invoke(this, dataStack);
        SetChoosePanelStatus(!IsSelect);
    }
    private void OnInfoButtonClick() {
        PopupHUD.Instance.Show<GearDetailItemPopup>().InitData(dataStack, null, false, true, OnChooseButtonClick);
    }
    private void OnSelectItem() {
        onFocus?.Invoke(this);
        if (!IsSelect) {
            SetChoosePanelStatus(!IsSelect, true);
        }
        else {
            IsSelect = !IsSelect;
            onSelect?.Invoke(this, dataStack);
        }
    }

    public IItem<GearSoftData> Generate() {
        SetTick(-1, -1, true);
        icon.SetAlpha(1f);
        icon.sprite = dataStack.GearHardData.GetIcon(dataStack.CurrentRank);
        frame.sprite = dataStack.GearHardData.GetRarety(dataStack.CurrentRank).Frame;
        if (levelText != null)
            levelText.text = $"Lv. {dataStack.CurrentLevel}";
        if (equipmentGO != null)
            equipmentGO.SetActive(dataStack.IsEquiped);
        //if(notice != null) notice.SetActive(!IsSelect && GameResources.Instance.GearInventory.GearHasCombo(data.GearHardData.Id, data.CurrentRank));
        return this;
    }
    public void SetTick(int id, int rank, bool defaultValue = false, bool disableAll = false) {
        if (IsSelect) {
            if (disableAll) {
                gameObject.SetActive(false);
                return;
            }
            if (tick != null)
                tick.SetActive(true);
            if (locked != null) {
                locked.gameObject.SetActive(true);
                locked.SetAlpha(0.7f);
                icon.SetAlpha(0.7f);
            }
            //if(notice != null) notice.SetActive(true);
            UpdateSelectButtonStatus(false);
        }
        else {
            bool condition = false;
            if (dataStack == null)
                defaultValue = true;
            else
                condition = dataStack.GearHardData.Id == id && dataStack.CurrentRank == rank;
            if (tick != null)
                tick.SetActive(defaultValue ? false : condition && IsSelect);
            if (locked != null) {
                locked.gameObject.SetActive(defaultValue ? false : !condition);
                if (condition) {
                    icon.SetAlpha(1);
                }
                else {
                    locked.SetAlpha(0.3f);
                    icon.SetAlpha(0.3f);
                }
            }
            UpdateSelectButtonStatus(defaultValue ? true : condition);
        }
        //gameObject.SetActive(!IsSelect);
    }
    public void SetTick1(int id, int rank, bool defaultValue = false, bool disableAll = false) {
        if (disableAll) {
            if (tick != null)
                tick.SetActive(false);
            if (locked != null)
                locked.gameObject.SetActive(true);
            //if(notice != null) notice.SetActive(false);
            UpdateSelectButtonStatus(false);
        }
        else {
            bool condition = false;
            if (dataStack == null)
                defaultValue = true;
            else
                condition = dataStack.GearHardData.Id == id && dataStack.CurrentRank == rank;
            if (tick != null)
                tick.SetActive(defaultValue ? false : condition);
            if (locked != null)
                locked.gameObject.SetActive(defaultValue ? false : !condition);
            UpdateSelectButtonStatus(defaultValue ? true : condition);
        }
    }
    public void ResetState() {
        icon.sprite = null;
        frame.sprite = null;
        SetTick(-1, -1, true);
    }
    public void UpdateIconFrameUI(Sprite iconSprite, Sprite frameSprite) {
        frame.sprite = frameSprite;
        icon.sprite = iconSprite;
        icon.gameObject.SetActive(iconSprite != null);
    }
    public void UpdateSelectButtonStatus(bool active) {
        if (selectButton != null)
            selectButton.interactable = active;
    }
}
