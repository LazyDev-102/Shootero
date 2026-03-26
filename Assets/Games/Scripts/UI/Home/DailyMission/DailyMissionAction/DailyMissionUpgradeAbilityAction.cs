using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionUpgradeAbilityAction", menuName = "Resource/GameAction/DailyMission/DailyMissionUpgradeAbilityAction")]
public class DailyMissionUpgradeAbilityAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowAbilityPanel();
    }
}