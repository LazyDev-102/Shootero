using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewAbilityItemView : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private ButtonExplorer selectButton;
    [SerializeField] private GameObject lockGo;
    [SerializeField] private Image connect;
    [SerializeField] private NewAbilityItemData data;

    private Action onClosed;

    private void Start() {
        selectButton.AddEvent(OnSelect);
    }

    public void Initialize(Action onClosed) {
        this.onClosed = onClosed;
        UpdateUI();
    }

    private void UpdateUI() {
        if (data.IsSpecial) {
            levelText.gameObject.SetActive(false);
        }
        bool unlocked = data.Unlocked;
        lockGo.SetActive(!unlocked);
        connect.SetAlpha(unlocked ? 1 : .3f);
        icon.sprite = data.Icon;
        levelText.text = data.IsMaxLevel ? "Max" : $"Lv.{data.Level}";
    }

    private void OnSelect() {
        PopupHUD.Instance.Show<AbilityItemInfoPopup>()
                         .Initialize(data, OnCloseInfoPopup);
    }

    private void OnCloseInfoPopup() {
        onClosed?.Invoke();
        UpdateUI();
    }
}
