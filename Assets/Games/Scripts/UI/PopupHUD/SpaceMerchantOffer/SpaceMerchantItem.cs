using DG.Tweening;
using Gear_Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpaceMerchantItem : MonoBehaviour {
    [SerializeField] private Image frame;
    [SerializeField] private Image icon;
    [SerializeField] private Image priceIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject likeGO;
    [SerializeField] private ButtonExplorer buyButton;
    [SerializeField] private WhiteFrameEffect showEffect;
    [SerializeField] private Image blackBg;
    [SerializeField] private Image tick;

    private GearSoftData data;
    private GearHardData hardData;
    private ItemStack price;
    private int rank;
    public void Assign() {
        buyButton.AddEvent(OnBuy);
    }
    public void UpdateUI(GearSoftData data, ItemStack price) {
        SetData(data, price);
        SetActive();
        SetIcon();
        SetFrame();
        SetBuy(true);
        SetPrice(true);
        SetName(true);
        ShowEffect();
        PlayEffect(false);
    }
    private void SetData(GearSoftData data, ItemStack price) {
        this.data = data;
        this.price = price;
        rank = data.CurrentRank;
        hardData = data.GearHardData;
    }
    private void SetActive() {
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 1.2f;
        transform.DOScale(1, 0.2f).SetUpdate(true);
    }
    private void SetIcon() {
        if (icon) {
            icon.sprite = hardData.GetIcon(rank);
        }
    }
    private void SetFrame() {
        if (icon) {
            frame.sprite = hardData.GetRarety(rank).Frame;
        }
    }
    private void SetPrice(bool show) {
        if (icon) {
            priceIcon.gameObject.SetActive(show);
            priceText.gameObject.SetActive(show);
            priceIcon.sprite = price.Icon;
            priceText.text = $"{ price.Amount}";
        }
    }
    private void SetName(bool show) {
        if (itemName) {
            itemName.gameObject.SetActive(show);
            itemName.text = hardData.Name;
            itemName.color = hardData.GetRarety(rank).Color;
        }
    }
    private void ShowEffect() {
        showEffect.Show(ShowHasCombo);
    }
    private void ShowHasCombo() {
        if (likeGO) {
            likeGO.SetActive(GameResources.Instance.GearInventory.GearCanCombo(data.Id, data.CurrentRank));
        }
    }
    private void OnBuy() {
        GameResources.Instance.Inventory.EnoughPrice(price, () => {
            hardData.AddNewGear(data.CurrentRank);
            SetPrice(false);
            SetName(false);
            SetBuy(false);
            PlayEffect(true);

        }, () => {
            NotificationText.Instance.Show("Not enough Gem!", NotificationText.NoticeType.Error);
        });
    }
    private void PlayEffect(bool bought) {
        blackBg.gameObject.SetActive(bought);
        tick.gameObject.SetActive(bought);
        if (!bought)
            return;
        blackBg.SetAlpha(0);
        tick.SetAlpha(0);
        blackBg.DOFade(0.7f, 0.5f).OnComplete(() => {
            tick.DOFade(1, 0.5f);
        });
    }
    private void SetBuy(bool interactable) {
        if (buyButton) {
            buyButton.SetState(interactable);
        }
    }
}
