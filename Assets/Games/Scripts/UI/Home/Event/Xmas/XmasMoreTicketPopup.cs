using GameSystem.Common.UI;
using System;
using TMPro;
using UnityEngine;

public class XmasMoreTicketPopup : DOTweenFrame
{
    [SerializeField] private ButtonBase buyButton;
    [SerializeField] private ButtonBase tab2HideButton;
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private TextMeshProUGUI remainText;
    [SerializeField] private TextMeshProUGUI rewardAmountText;
    [SerializeField] private TextMeshProUGUI priceText;

    private XmasModeData data;

    private void Awake() {
        SetData();
        AddEvent();
    }
    private void AddEvent() {
        buyButton.AddEvent(Buy);
        tab2HideButton.AddEvent(Close);
        closeButton.AddEvent(Close);
    }
    private void SetData() {
        data = GameResources.Instance.Xmas;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
    }

    private void UpdateUI() {
        remainText.text = $"Remaining to day: {data.BuyableRemain}";
        priceText.text = $"{data.MoreTicketPrice.Amount}";
        rewardAmountText.text = $"{data.MoreTicketReward.Amount}";
        buyButton.SetState(data.Buyable);
    }

    private void Buy() {
        GameResources.Instance.Inventory.EnoughPrice(data.MoreTicketPrice, () => {
            data.BuyMoreTicket();
            UpdateUI();
            var xmas = PanelHUD.Instance.GetActiveFrame<XmasPanel>();
            if(xmas != null) {
                xmas.UpdateUI();
            }
            NotificationUI.Instance.SetContent(GameDefine.Success, 0.5f)
                                   .Show();
        }, () => {
            NotificationUI.Instance.SetContent(GameDefine.InsufficientResources, 0.5f)
                                   .Show();
        });
    }
    private void Close() {
        Hide();
    }
}
