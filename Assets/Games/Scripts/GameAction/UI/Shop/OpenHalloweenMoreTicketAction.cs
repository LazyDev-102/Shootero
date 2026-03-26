

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OpenHalloweenMoreTicketAction", menuName = "Resource/GameAction/Shop/OpenHalloweenMoreTicketAction")]
public class OpenHalloweenMoreTicketAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.Show<HalloweenMoreTicketPopup>();
    }
}
