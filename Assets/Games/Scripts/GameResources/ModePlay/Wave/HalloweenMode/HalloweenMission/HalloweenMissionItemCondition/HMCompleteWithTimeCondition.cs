using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HMCompleteWithTimeCondition", menuName = "Resource/Conditions/Halloween/CompleteWithTimeCondition")]
public class HMCompleteWithTimeCondition : WaveCondition<HalloweenMissionItemData> {
    public override bool Action(HalloweenMissionItemData target, Action onComplete) {
        var condition = CheckCondition(target);
        if (condition && action != null)
            action.Execute(target, onComplete);
        return condition;
    }

    public override bool CheckCondition(HalloweenMissionItemData mission) {
        if (IngameData.currentGameMode == GameMode.EventHalloween) {
            return IngameHUD.Instance.Combat.GetTime() >= mission.PointTarget;
        }
        else
            return false;

    }

    public override bool CheckCondition(object target) {
        HalloweenMissionItemData convertData = (HalloweenMissionItemData)target;
        if (convertData != null && IngameData.currentGameMode == GameMode.EventHalloween) {
            return IngameHUD.Instance.Combat.GetTime() >= convertData.PointTarget;
        }
        else
            return false;
    }
}
