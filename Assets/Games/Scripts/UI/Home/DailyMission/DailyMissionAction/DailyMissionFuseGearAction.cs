using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionFuseGearAction", menuName = "Resource/GameAction/DailyMission/DailyMissionFuseGearAction")]
public class DailyMissionFuseGearAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowGearPanel();
    }
}