using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionDefeatEnemyAction", menuName = "Resource/GameAction/DailyMission/DailyMissionDefeatEnemyAction")]
public class DailyMissionDefeatEnemyAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowConquerorPanel();
    }
}