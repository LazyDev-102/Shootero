using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionDefeatBossAction", menuName = "Resource/GameAction/DailyMission/DailyMissionDefeatBossAction")]
public class DailyMissionDefeatBossAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowConquerorPanel();
    }
}