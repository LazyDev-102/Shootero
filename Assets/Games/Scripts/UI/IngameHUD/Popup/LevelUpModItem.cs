using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;

public class LevelUpModItem : MonoBehaviour, IItem<ModData> {
    [SerializeField] private Image modIcon;
    [SerializeField] private Image infoHeader;
    [SerializeField] private Image infoBackground;
    [SerializeField] private Image whiteBackground;
    [SerializeField] private TextMeshProUGUI modName;
    [SerializeField] private TextMeshProUGUI modDescription;
    [SerializeField] private ButtonExplorer selectButton;
    [SerializeField] private GameObject infoPanel;

    private Action<bool> onSelect;

    public ModData dataStack { get; set; }

    private void Awake() {
        selectButton.AddEvent(OnSelectButtonClick);
    }
    public void Initialized(ModData data) {
        this.dataStack = data;
        transform.localScale = Vector3.zero;
        Generate();
    }
    public IItem<ModData> Generate() {
        SetIcon(dataStack.Icon, true);
        SetModName(dataStack.NameMod, true);
        SetModDescription(dataStack.ModDescription, true);
        return this;
    }
    public LevelUpModItem OnSelect(Action<bool> onSelect) {
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
        if (modIcon) {
            modIcon.gameObject.SetActive(show);
            if (show) {
                modIcon.sprite = icon;
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
    public void PlayEffect() {
        if (whiteBackground) {
            whiteBackground.SetAlpha(1);
            whiteBackground.DOFade(0, 0.3f).SetUpdate(true);
            transform.localScale = Vector3.one * 1.2f;
            transform.DOScale(1, 0.3f).SetUpdate(true);
        }
    }

    private void OnSelectButtonClick() {
        var status = !infoPanel.activeInHierarchy;
        SetInfoPanelStatus(status);
        onSelect?.Invoke(status);
        transform.DOScale(Vector3.one * 0.95f, 0.1f).SetLoops(2, LoopType.Yoyo);
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
