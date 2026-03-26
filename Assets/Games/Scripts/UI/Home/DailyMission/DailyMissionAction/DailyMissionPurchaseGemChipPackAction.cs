using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionPurchaseGemChipPackAction", menuName = "Resource/GameAction/DailyMission/DailyMissionPurchaseGemChipPackAction")]
public class DailyMissionPurchaseGemChipPackAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowShopPanel();
        PanelHUD.Instance.Shop.FocusGem();
    }
}