using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionCompleteChallengeAction", menuName = "Resource/GameAction/DailyMission/DailyMissionCompleteChallengeAction")]
public class DailyMissionCompleteChallengeAction : GameAction {
    public override void Execute(object user, Action onCompleted) {
        PopupHUD.Instance.Mission.OpenPage(false);
    }
}