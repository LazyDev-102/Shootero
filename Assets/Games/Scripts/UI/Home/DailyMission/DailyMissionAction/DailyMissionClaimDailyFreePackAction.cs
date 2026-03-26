using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionClaimDailyFreePackAction", menuName = "Resource/GameAction/DailyMission/DailyMissionClaimDailyFreePackAction")]
public class DailyMissionClaimDailyFreePackAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowShopPanel();
        PanelHUD.Instance.Shop.FocusDailyFreePack();
    }
}