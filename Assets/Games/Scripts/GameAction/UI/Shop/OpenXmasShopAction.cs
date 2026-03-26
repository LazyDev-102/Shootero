

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OpenXmasShopAction", menuName = "Resource/GameAction/Shop/OpenXmasShopAction")]
public class OpenXmasShopAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PanelHUD.Instance.Show<XmasShopPanel>();
    }
}
