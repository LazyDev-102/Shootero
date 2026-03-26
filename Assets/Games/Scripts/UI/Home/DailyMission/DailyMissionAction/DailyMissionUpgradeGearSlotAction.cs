using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionUpgradeGearSlotAction", menuName = "Resource/GameAction/DailyMission/DailyMissionUpgradeGearSlotAction")]
public class DailyMissionUpgradeGearSlotAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowGearPanel();
    }
}