

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OpenChipShopAction", menuName = "Resource/GameAction/Shop/OpenChipShop")]
public class OpenChipShopAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        if (PanelHUD.Instance.GetFrameOnTop() != PanelHUD.Instance.Shop)
            PanelHUD.Instance.Hide();
        ToolbarScaler.Instance.ShowShopPanel();
        PanelHUD.Instance.Shop.FocusChip();
    }
}
