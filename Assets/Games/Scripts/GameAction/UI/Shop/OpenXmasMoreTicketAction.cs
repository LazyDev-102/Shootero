

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OpenXmasMoreTicketAction", menuName = "Resource/GameAction/Shop/OpenXmasMoreTicketAction")]
public class OpenXmasMoreTicketAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.Show<XmasMoreTicketPopup>();
    }
}
