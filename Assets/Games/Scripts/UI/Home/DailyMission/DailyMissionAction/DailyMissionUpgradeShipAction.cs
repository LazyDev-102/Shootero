using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionUpgradeShipAction", menuName = "Resource/GameAction/DailyMission/DailyMissionUpgradeShipAction")]
public class DailyMissionUpgradeShipAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowGearPanel();
        PanelHUD.Instance.Show<ShipPanel>(pauseCurrent: true);
    }
}