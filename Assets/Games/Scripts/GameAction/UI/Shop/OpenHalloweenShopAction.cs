

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OpenHalloweenShopAction", menuName = "Resource/GameAction/Shop/OpenHalloweenShopAction")]
public class OpenHalloweenShopAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PanelHUD.Instance.Show<HalloweenShopPanel>();
    }
}
