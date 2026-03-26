using Gemmob;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HalloweenPackItem : MonoBehaviour, IItem<HalloweenPackItemData>
{
    [SerializeField] private TextMeshProUGUI remainExchangeText;
    [SerializeField] private ButtonBase exchangeButton;
    [SerializeField] private Image sourceImage;
    [SerializeField] private Image destinationImage;
    [SerializeField] private TextMeshProUGUI sourceValueText;
    [SerializeField] private TextMeshProUGUI destinationValueText;
    [SerializeField] private GameObject lockFrame;

    public HalloweenPackItemData dataStack { get ; set ; }

    private void Start() {
        exchangeButton.AddEvent(Exchange);
    }

    public void Initialize(HalloweenPackItemData data) {
        SetData(data);
        Generate();
    }
    private void SetData(HalloweenPackItemData data) {
        dataStack = data;
    }
    public IItem<HalloweenPackItemData> Generate() {
        bool buyable = dataStack.Buyable && GameResources.Instance.Inventory.EnoughPrice(dataStack.Price);
        remainExchangeText.text = dataStack.GetRemainExChange();
        remainExchangeText.color = dataStack.Buyable ? Color.white : Color.red;
        sourceImage.sprite = dataStack.Price.Icon;
        destinationImage.sprite = dataStack.ItemClaims[0].Icon;
        sourceValueText.text = $"{dataStack.Price.Amount}";
        sourceValueText.color = buyable ? Color.green : Color.red;
        destinationValueText.text = $"{dataStack.ItemClaims[0].Amount}";
        exchangeButton.SetState(buyable);
        lockFrame.SetActive(!buyable);
        return this;
    }

    private void Exchange() {
        GameResources.Instance.Inventory.EnoughPrice(dataStack.Price, ()=> {
            dataStack.Claim(1);
            Generate();
            PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(dataStack.ItemClaims);
            EventDispatcher.Instance.Dispatch(EventKey.OnExchangeHalloweenCandy);
            //NotificationUI.Instance.SetContent(GameDefine.ExchangeSuccess, 0.5f)
            //                        .Show();
        }, ()=> {
            NotificationUI.Instance.SetContent(GameDefine.InsufficientResources, 0.5f)
                                   .Show();
        });
    }
}
