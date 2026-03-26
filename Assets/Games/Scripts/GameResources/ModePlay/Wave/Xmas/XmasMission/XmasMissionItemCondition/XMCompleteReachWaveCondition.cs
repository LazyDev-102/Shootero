using System;
using UnityEngine;

[CreateAssetMenu(fileName = "XMCompleteReachWaveCondition", menuName = "Resource/Conditions/Xmas/CompleteReachWaveCondition")]
public class XMCompleteReachWaveCondition : WaveCondition<XmasMissionItemData> {
    public override bool Action(XmasMissionItemData target, Action onComplete) {
        var condition = CheckCondition(target);
        if (condition && action != null)
            action.Execute(target, onComplete);
        return condition;
    }

    public override bool CheckCondition(XmasMissionItemData mission) {
        return GameResources.Instance.Xmas.CurrentWave > mission.PointTarget;

    }

    public override bool CheckCondition(object target) {
        XmasMissionItemData convertData = (XmasMissionItemData)target;
        if (convertData != null) {
            return GameResources.Instance.Xmas.CurrentWave > convertData.PointTarget;
        }
        else
            return false;
    }
}
