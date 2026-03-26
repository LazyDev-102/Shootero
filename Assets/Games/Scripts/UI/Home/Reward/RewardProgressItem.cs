using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardProgressItem : ItemBase<ItemStack> {
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI valueText;

    private ItemStack data;

    public RewardProgressItem SetIcon(Sprite sprite) {
        icon.sprite = sprite;
        return this;
    }

    public RewardProgressItem SetValue(string value) {
        valueText.text = value;
        return this;
    }

    public override void UpdateUI(ContainerBase<ItemStack> view, ItemStack data) {
        base.UpdateUI(view, data);
        this.data = data;
        UpdateUI();
    }
    private void UpdateUI() {
        icon.sprite = data.Icon;
        valueText.text = data.Amount.ToString();
    }
}
