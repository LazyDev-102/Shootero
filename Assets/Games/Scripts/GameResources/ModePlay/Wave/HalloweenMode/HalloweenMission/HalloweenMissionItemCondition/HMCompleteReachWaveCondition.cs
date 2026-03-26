using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HMCompleteReachWaveCondition", menuName = "Resource/Conditions/Halloween/CompleteReachWaveCondition")]
public class HMCompleteReachWaveCondition : WaveCondition<HalloweenMissionItemData> {
    public override bool Action(HalloweenMissionItemData target, Action onComplete) {
        var condition = CheckCondition(target);
        if (condition && action != null)
            action.Execute(target, onComplete);
        return condition;
    }

    public override bool CheckCondition(HalloweenMissionItemData mission) {
        return GameResources.Instance.Halloween.CurrentWave > mission.PointTarget; 

    }

    public override bool CheckCondition(object target) {
        HalloweenMissionItemData convertData = (HalloweenMissionItemData)target;
        if(convertData != null) {
            return GameResources.Instance.Halloween.CurrentWave > convertData.PointTarget;
        }else
        return false;
    }
}
