using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItem : MonoBehaviour, IItem<ItemStack> {
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;
    [SerializeField] private Image frame;

    public ItemStack dataStack { get; set; }
    public ItemClaim dataClaim { get; set; }

    public void UpdateUI(ItemStack data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, int multi = 1) {
        this.dataStack = data;
        SetName(data.Name, useName);
        SetAmountText($"{data.Amount * multi}", useAmount);
        SetIcon(data.Icon, useIcon);
        SetDescriptionText(data.Description, useDescription);
        PlayEffect();
    }
    public void UpdateUI(ItemClaim data, bool useName = false, bool useAmount = true, bool useIcon = true, bool useDescription = false, int multi = 1) {
        this.dataClaim = data;
        SetName(data.Name, useName);
        SetAmountText($"{data.Amount * multi}", useAmount);
        SetIcon(data.Icon, useIcon);
        SetDescriptionText(data.Description, useDescription);
        PlayEffect();
    }
    public RewardItem SetName(string name, bool status) {
        if (nameText) {
            nameText.gameObject.SetActive(status);
            nameText.text = name;
        }
        return this;
    }
    public RewardItem SetAmountText(string amount, bool status) {
        if (amountText) {
            amountText.gameObject.SetActive(status);
            amountText.text = amount;
        }
        return this;
    }
    public RewardItem SetDescriptionText(string description, bool status) {
        if (descriptionText) {
            descriptionText.gameObject.SetActive(status);
            descriptionText.text = description;
        }
        return this;
    }
    public RewardItem SetIcon(Sprite sprite, bool status) {
        if (icon) {
            icon.gameObject.SetActive(status);
            icon.sprite = sprite;
        }
        return this;
    }
    public RewardItem SetFrame(Sprite sprite, bool status) {
        if (frame) {
            frame.gameObject.SetActive(status);
            frame.sprite = sprite;
        }
        return this;
    }
    private void PlayEffect() {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.3f);
    }
    public IItem<ItemStack> Generate() {
        return this;
    }
}
