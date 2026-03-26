using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionClaimDailyPacksAction", menuName = "Resource/GameAction/DailyMission/DailyMissionClaimDailyPacksAction")]
public class DailyMissionClaimDailyPacksAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowConquerorPanel();
        PanelHUD.Instance.Conqueror.OpenDailyPacksPopup();
    }
}