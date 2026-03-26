using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionPlayConquerorAction", menuName = "Resource/GameAction/DailyMission/DailyMissionPlayConquerorAction")]
public class DailyMissionPlayConquerorAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowConquerorPanel();
    }
}