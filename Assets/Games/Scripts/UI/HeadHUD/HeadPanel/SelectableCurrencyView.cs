using GameSystem.Common.UnityInspector;
using Gemmob;
using UnityEngine;

public class SelectableCurrencyView : MonoBehaviour {
    [SerializeField, ItemField] private int itemID;
    [SerializeField] private CurrencyView currencyView;
    [SerializeField] private ButtonBase btnSelect;
    [SerializeField] private GameAction selectAction;
    [SerializeField] private GameObject buyIcon;
    public Transform TargetUI;

    protected virtual void Start() {
        EventDispatcher.Instance.AddListener(EventKey.OnChipChanged, OnChipChanged);
        EventDispatcher.Instance.AddListener(EventKey.OnGemChanged, OnGemChanged);
        EventDispatcher.Instance.AddListener(EventKey.OnEnergyChanged, OnEnergyChanged);
        btnSelect?.AddEvent(OnSelectButtonClicked);
        Show();
    }
    private void OnEnable() {
        Show();
    }
    protected virtual void OnDestroy() {
        if (EventDispatcher.Initialized) {
            EventDispatcher.Instance.RemoveListener(EventKey.OnChipChanged, OnChipChanged);
            EventDispatcher.Instance.RemoveListener(EventKey.OnGemChanged, OnGemChanged);
            EventDispatcher.Instance.RemoveListener(EventKey.OnEnergyChanged, OnEnergyChanged);
        }
    }

    public void Show() {
        ItemStack itemStack = GameResources.Instance.Inventory.GetItem(itemID);
        SetItem(itemStack);
    }

    public virtual void SetItem(IItemInstance item) {
        if (currencyView) {
            currencyView.SetModel(item).Show();
        }
    }

    public void OnChipChanged() {
        if (itemID == ConstantItemID.ChipId) {
            ItemStack itemStack = GameResources.Instance.Inventory.GetItem(itemID);
            SetItem(itemStack);
        }
    }
    public void OnGemChanged() {
        if (itemID == ConstantItemID.GemId) {
            ItemStack itemStack = GameResources.Instance.Inventory.GetItem(itemID);
            SetItem(itemStack);
        }
    }
    public void OnEnergyChanged() {
        if (itemID == ConstantItemID.EnergyId) {
            ItemStack itemStack = GameResources.Instance.Inventory.GetItem(itemID);
            SetItem(itemStack);
        }
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
            selectAction.Execute(this);
            if (itemID != ConstantItemID.EnergyId)
                ToolbarScaler.Instance.SetActive(true);
        }
    }
}
