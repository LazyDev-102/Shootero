using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionOpenNormalChestAction", menuName = "Resource/GameAction/DailyMission/DailyMissionOpenNormalChestAction")]
public class DailyMissionOpenNormalChestAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowShopPanel();
        PanelHUD.Instance.Shop.FocusChest();
    }
}