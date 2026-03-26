using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionBuyEnergyAction", menuName = "Resource/GameAction/DailyMission/DailyMissionBuyEnergyAction")]
public class DailyMissionBuyEnergyAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        PopupHUD.Instance.Show<MoreEnergyPopup>();
    }
}