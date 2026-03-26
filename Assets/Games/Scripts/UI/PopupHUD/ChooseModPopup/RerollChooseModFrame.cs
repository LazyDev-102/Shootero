using System;
using TMPro;
using UnityEngine;

public class RerollChooseModFrame : MonoBehaviour {
    [SerializeField] private Item rerollItem;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private ButtonExplorer rerollButton;
    [SerializeField] private ButtonExplorer freeButton;
    [SerializeField] private AbilityFreeReroll freeReroll;

    private ChooseModPopup chooseMod;
    private Inventory inv;
    private Action hideOnRoll;
    private int cRerollCount;
    private bool oneTimes;

    private void Start() {
        rerollButton.AddEvent(OnReroll);
        freeButton.AddEvent(OnFreeReroll);
    }
    private void OnDisable() {
        oneTimes = false;
    }
    public RerollChooseModFrame SetRef(ChooseModPopup chooseMod) {
        this.chooseMod = chooseMod;
        if (inv == null) {
            inv = GameResources.Instance.Inventory;
        }
        return this;
    }

    public RerollChooseModFrame Active(Action hideOnRoll) {
        UpdateUI(inv.GetItem(rerollItem.Id).Amount);
        rerollButton.SetState(true);
        this.hideOnRoll = hideOnRoll;
        oneTimes = true;
        return this;
    }
    private void UpdateUI(int value) {
        bool free = !oneTimes && freeReroll.Active;
        gameObject.SetActive(value > 0);
        freeButton.gameObject.SetActive(free);
        rerollButton.gameObject.SetActive(!free);
        valueText.text = $"{value}";
    }
    private void OnReroll() {
        cRerollCount++;
        hideOnRoll?.Invoke();
        rerollButton.SetState(false);
        chooseMod.RerollMods(cRerollCount % 3 == 0);
        inv.Remove(rerollItem.Id, 1);
        UpdateUI(inv.GetItem(rerollItem.Id).Amount);
    }
    private void OnFreeReroll() {
        cRerollCount++;
        hideOnRoll?.Invoke();
        rerollButton.SetState(false);
        chooseMod.RerollMods(cRerollCount % 3 == 0);
        UpdateUI(inv.GetItem(rerollItem.Id).Amount);
    }
}
