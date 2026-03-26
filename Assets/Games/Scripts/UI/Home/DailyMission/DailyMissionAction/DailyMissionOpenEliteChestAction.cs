using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionOpenEliteChestAction", menuName = "Resource/GameAction/DailyMission/DailyMissionOpenEliteChestAction")]
public class DailyMissionOpenEliteChestAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowShopPanel();
        PanelHUD.Instance.Shop.FocusChest();
    }
}