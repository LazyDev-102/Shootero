
using Gemmob;
using UnityEngine;

public class HTicketCurrencyView : MonoBehaviour {
    [SerializeField] private CurrencyView currencyView;
    [SerializeField] private ButtonBase btnSelect;
    [SerializeField] private GameAction selectAction;
    [SerializeField] private GameObject buyIcon;

    protected virtual void Start() {
        EventDispatcher.Instance.AddListener(EventKey.OnHTicketChanged, OnHTicketChanged);
        btnSelect?.AddEvent(OnSelectButtonClicked);
        Show();
    }
    private void OnEnable() {
        Show();
    }
    protected virtual void OnDestroy() {
        if (EventDispatcher.Initialized) {
            EventDispatcher.Instance.RemoveListener(EventKey.OnHTicketChanged, OnHTicketChanged);
        }
    }

    public void Show() {
        ItemStack itemStack = GameResources.Instance.Inventory.GetHTicket();
        SetItem(itemStack);
    }

    public virtual void SetItem(IItemInstance item) {
        if (currencyView) {
            currencyView.SetModel(item).Show();
        }
    }

    public void OnHTicketChanged() {
        ItemStack itemStack = GameResources.Instance.Inventory.GetHTicket();
        SetItem(itemStack);
    }

    public void SetSelectButtonState(bool interacable, bool show = true) {
        if (btnSelect) {
            btnSelect.gameObject.SetActive(show);
            if (show) {
                buyIcon.gameObject.SetActive(interacable);
                btnSelect.SetState(interacable);
            }
        }
    }

    protected void OnSelectButtonClicked() {
        if (selectAction) {
            selectAction.Execute(this); ;
        }
    }
}
