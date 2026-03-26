
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "OpenBuyEnergyAction", menuName = "Resource/GameAction/Shop/OpenBuyEnergy")]
public class OpenBuyEnergyAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        PopupHUD.Instance.Show<MoreEnergyPopup>();
    }
}
