using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseModView : View<ModData> {
    [SerializeField] private Image imgIcon;
    [SerializeField] private Image infoHeader;
    [SerializeField] private Image infoBackground;
    [SerializeField] private ButtonExplorer selectButton;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI modName;
    [SerializeField] private TextMeshProUGUI modDescription;

    private Action<bool> onSelect;

    private void Awake() {
        selectButton.AddEvent(OnSelectButtonClick);
    }
    public override void Show() {
        if (Model == null) {
            return;
        }
        SetIcon(Model.Icon, true);
        SetModName(Model.NameMod, true);
        SetModDescription(Model.ModDescription, true);
    }
    public PauseModView OnSelect(Action<bool> onSelect) {
        this.onSelect = onSelect;
        return this;
    }
    public void OnDeSelect() {
        var canvas = gameObject.GetComponent<Canvas>();
        if (canvas != null) {
            infoPanel.SetActive(false);
            Destroy(canvas);
        }
    }
    public void SetIcon(Sprite icon, bool show) {
        if (imgIcon) {
            imgIcon.gameObject.SetActive(show);
            if (show) {
                imgIcon.sprite = icon;
            }
        }
    }
    public void SetModName(string name, bool show) {
        if (modName) {
            modName.gameObject.SetActive(show);
            if (show) {
                modName.text = name;
            }
        }
    }
    public void SetModDescription(string description, bool show) {
        if (modDescription) {
            modDescription.gameObject.SetActive(show);
            if (show) {
                modDescription.text = description;
            }
        }
    }

    private void OnSelectButtonClick() {
        var status = !infoPanel.activeInHierarchy;
        SetInfoPanelStatus(status);
        onSelect?.Invoke(status);
    }
    private void SetInfoPanelStatus(bool status) {
        infoPanel.SetActive(status);
        if (status) {
            AddCanvas();
            infoHeader.SetAlpha(0);
            infoHeader.DOFade(0.5f, 0.3f).SetUpdate(true);
            infoBackground.SetAlpha(0);
            infoBackground.DOFade(0.3f, 0.3f).SetUpdate(true);
        }
    }
    private void AddCanvas() {
        var canvas = gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 999;
        canvas.sortingLayerName = GameLayer.UI;
    }
}
