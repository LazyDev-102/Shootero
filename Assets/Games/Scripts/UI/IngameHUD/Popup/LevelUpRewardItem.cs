using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpRewardItem : MonoBehaviour, IItem<ItemStack> {
    [SerializeField] private Image icon;
    [SerializeField] private Image whiteBackground;
    [SerializeField] private TextMeshProUGUI valueText;

    public ItemStack dataStack { get; set; }

    public IItem<ItemStack> Generate() {
        icon.sprite = dataStack.Icon;
        valueText.text = dataStack.Amount.ToString();
        //icon.SetNativeSize();
        return this;
    }
    public void Initialized(ItemStack data) {
        this.dataStack = data;
        transform.localScale = Vector3.zero;
        Generate();
    }
    public void PlayEffect() {
        if (whiteBackground) {
            whiteBackground.SetAlpha(1);
            whiteBackground.DOFade(0, 0.2f).SetUpdate(true);
            transform.localScale = Vector3.one * 1.2f;
            transform.DOScale(1, 0.2f).SetUpdate(true);
        }
    }
}
