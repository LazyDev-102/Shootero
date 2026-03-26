using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AbilityInfoView : View<AbilityData> {
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI abilityName;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private TextMeshProUGUI txtStatInfo;
    [SerializeField] private ButtonBase btnClose;

    [Header("Upgrade Info View")]
    [SerializeField] private GameObject upgradeInfoBoard;
    [SerializeField] private GameObject normalInfoBoard;
    [SerializeField] private TextMeshProUGUI abilityDescriptionName;
    [SerializeField] private TextMeshProUGUI abilityPreValue;
    [SerializeField] private TextMeshProUGUI abilityAfterValue;

    private Action onClose;

    public void Start() {
        btnClose?.AddEvent(OnCloseButtonClicked);
    }

    public override void Show() {
        if (Model == null) {
            return;
        }
        SetInfoUpgradeBoard(false);
        SetContentIcon(Model.Icon, true);
        SetContentNameText(Model.AbilityName, true);
        string levelText = Model.IsMaxLevel ? "Level MAX" : $"Lv.{(Model.CurrentLevel + 1)}";
        SetContentLevelText(levelText, true);
        SetContentStatInfoText(Model.GetDescription(), true);
    }

    public void AddOnClose(Action onClose) {
        this.onClose = onClose;
    }

    private void OnCloseButtonClicked() {
        onClose?.Invoke();
    }

    public void SetContentNameText(string content, bool show) {
        if (txtName) {
            txtName.gameObject.SetActive(show);
            abilityName.gameObject.SetActive(show);
            if (show) {
                txtName.text = content;
                abilityName.text = content;
            }
        }
    }

    public void SetContentLevelText(string content, bool show) {
        if (txtLevel) {
            txtLevel.gameObject.SetActive(show);
            if (show) {
                txtLevel.text = content;
            }
        }
    }

    public void SetContentStatInfoText(string content, bool show) {
        if (txtStatInfo) {
            txtStatInfo.gameObject.SetActive(show);
            if (show) {
                txtStatInfo.text = content;
            }
        }
    }

    public void SetContentIcon(Sprite icon, bool show) {
        if (imgIcon) {
            imgIcon.gameObject.SetActive(show);
            if (show) {
                imgIcon.sprite = icon;
                imgIcon.SetNativeSize();
            }
        }
    }
    public AbilityInfoView SetInfoUpgradeBoard(bool status) {
        upgradeInfoBoard.SetActive(status);
        normalInfoBoard.SetActive(!status);
        if (status && abilityDescriptionName && abilityAfterValue && abilityPreValue) {
            abilityDescriptionName.text = Model.Description;
            abilityPreValue.text = Model.GetValueString(Model.CurrentLevel - 1);
            abilityAfterValue.text = Model.GetValueString(Model.CurrentLevel);
        }
        return this;
    }
}
