using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsItemView : MonoBehaviour, IItem<ItemSkillData> {
    [SerializeField] private Image skillIcon;
    [SerializeField] private ButtonExplorer selectButton;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject equiped;
    [SerializeField] private Image amountBg;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameObject notiNew;
    [SerializeField] private Material ownMat;
    [SerializeField] private Material notOwnMat;

    private Action<SkillsItemView> onSelected;

    public ItemSkillData dataStack { get; set; }

    private void Awake() {
        selectButton.AddEvent(OnSelect);
    }

    public IItem<ItemSkillData> Generate() {
        bool enough = dataStack.CanUpgradable();
        bool isOwn = dataStack.IsOwn;
        skillIcon.material = isOwn ? ownMat : notOwnMat;
        skillIcon.sprite = dataStack.Icon;
        equiped.SetActive(isOwn && dataStack.IsEquip());
        amountBg.gameObject.SetActive(isOwn);
        amountBg.color = enough ? Color.red : Color.white;
        amountText.color = enough ? Color.white : Color.black;
        amountText.text = dataStack.Amount.ToString();
        notiNew.SetActive(isOwn && dataStack.IsNew);
        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive(isOwn && i <= dataStack.Rank);
        }
        return this;
    }
    public void UpdateUI(ItemSkillData data, Action<SkillsItemView> onSelected) {
        dataStack = data;
        this.onSelected = onSelected;
        Generate();
    }
    private void OnSelect() {
        onSelected?.Invoke(this);
        dataStack.IsNew = false;
        notiNew.SetActive(false);
    }

}
