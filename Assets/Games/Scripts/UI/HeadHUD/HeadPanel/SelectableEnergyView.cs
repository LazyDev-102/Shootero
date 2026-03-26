using Gemmob;
using UnityEngine;

public class SelectableEnergyView : SelectableCurrencyView {
    protected override void Start() {
        base.Start();
        EventDispatcher.Instance.AddListener<EventKey.OnMaxEnergyChange>(OnEnergyChange);
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        EventDispatcher.Instance.RemoveListener<EventKey.OnMaxEnergyChange>(OnEnergyChange);
    }

    private void OnEnergyChange(EventKey.OnMaxEnergyChange param) {
        ItemStack itemStack = GameResources.Instance.Inventory.GetItem(ConstantItemID.EnergyId);
        SetItem(itemStack);
    }
}
