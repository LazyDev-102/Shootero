using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionClaimAFKRewardAction", menuName = "Resource/GameAction/DailyMission/DailyMissionClaimAFKRewardAction")]
public class DailyMissionClaimAFKRewardAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.HideAll();
        PopupHUD.Instance.Show<AfkPopup>().UpdateUI(GameResources.Instance.AFK);
    }
}