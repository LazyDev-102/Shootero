

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OpenGemShopAction", menuName = "Resource/GameAction/Shop/OpenGemShop")]
public class OpenGemShopAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        if (PanelHUD.Instance.GetFrameOnTop() != PanelHUD.Instance.Shop)
            PanelHUD.Instance.Hide();
        ToolbarScaler.Instance.ShowShopPanel();
        PanelHUD.Instance.Shop.FocusGem();
    }
}
