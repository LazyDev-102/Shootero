using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattlePassItem : MonoBehaviour, IItem<BattlePassItemData> {
    [SerializeField] private TextMeshProUGUI indexText;
    [SerializeField] private TextMeshProUGUI index1Text;
    [SerializeField] private GameObject indexIcon;
    [SerializeField] private GameObject index1Icon;

    [Header("Free")]
    [SerializeField] private Image freeIcon;
    [SerializeField] private Image freeBackground;
    [SerializeField] private GameObject freeLockIcon;
    [SerializeField] private ButtonBase freeClaimableIcon;
    [SerializeField] private TextMeshProUGUI freeValueText;

    [Header("Purchase")]
    [SerializeField] private Image purchaseIcon;
    [SerializeField] private Image purchaseBackground;
    [SerializeField] private GameObject purchaseLockIcon;
    [SerializeField] private ButtonBase purchaseClaimableIcon;
    [SerializeField] private TextMeshProUGUI purchaseValueText;
    public BattlePassItemData dataStack { get; set; }
    private Action onClaim;
    private void Awake() {
        freeClaimableIcon.AddEvent(FreeClaimReward);
        purchaseClaimableIcon.AddEvent(PurchaseClaimReward);
    }
    public void Initialized(BattlePassItemData data, Action onClaim) {
        dataStack = data;
        this.onClaim = onClaim;
        Generate();
    }

    public IItem<BattlePassItemData> Generate() {
        indexText.text = $"{dataStack.Index + 1}";
        index1Text.text = $"{dataStack.Index + 1}";
        indexIcon.SetActive(dataStack.IsComplete);
        index1Icon.SetActive(!dataStack.IsComplete);
        UpdateFreeGroup();
        UpdatePurchaseGroup();
        return this;
    }
    private void UpdateFreeGroup() {
        var free = dataStack.FreeReward;
        freeIcon.sprite = free.Icon;
        freeLockIcon.SetActive(!dataStack.FreeClaimable && !dataStack.IsComplete);
        freeValueText.text = $"{free.Amount}";
        freeClaimableIcon.gameObject.SetActive(GameResources.Instance.BattlePass.Claimable(dataStack.Index, true));
        if (dataStack.FreeClamed) {
            freeIcon.SetAlpha(0.3f);
            freeValueText.SetAlpha(0.3f);
            freeBackground.SetAlpha(0.3f);
        }
        else {
            freeIcon.SetAlpha(1);
            freeValueText.SetAlpha(1);
            freeBackground.SetAlpha(1);
        }
    }
    private void UpdatePurchaseGroup() {
        var purchase = dataStack.PurchaseReward;
        purchaseIcon.sprite = purchase.Icon;
        purchaseValueText.text = $"{purchase.Amount}";
        purchaseLockIcon.SetActive(!dataStack.PurchaseClaimable && !dataStack.IsComplete);
        purchaseClaimableIcon.gameObject.SetActive(GameResources.Instance.BattlePass.Claimable(dataStack.Index, false));
        if (!GameResources.Instance.BattlePass.IsPurchase)
            purchaseLockIcon.SetActive(true);
        if (dataStack.PurchaseClamed) {
            purchaseIcon.SetAlpha(0.3f);
            purchaseValueText.SetAlpha(0.3f);
            purchaseBackground.SetAlpha(0.3f);
        }
        else {
            purchaseIcon.SetAlpha(1);
            purchaseValueText.SetAlpha(1);
            purchaseBackground.SetAlpha(1);
        }
    }
    private void FreeClaimReward() {
        dataStack.ClaimFreeReward();
        UpdateFreeGroup();
        PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(new System.Collections.Generic.List<ItemClaim>() { dataStack.FreeReward });
        onClaim?.Invoke();
    }
    private void PurchaseClaimReward() {
        dataStack.ClaimPurchaseReward();
        UpdatePurchaseGroup();
        PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(new System.Collections.Generic.List<ItemClaim>() { dataStack.PurchaseReward });
        onClaim?.Invoke();
    }
}
