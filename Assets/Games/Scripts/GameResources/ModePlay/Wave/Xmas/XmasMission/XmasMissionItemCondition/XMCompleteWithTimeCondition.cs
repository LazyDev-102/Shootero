using System;
using UnityEngine;

[CreateAssetMenu(fileName = "XMCompleteWithTimeCondition", menuName = "Resource/Conditions/Xmas/CompleteWithTimeCondition")]
public class XMCompleteWithTimeCondition : WaveCondition<XmasMissionItemData> {
    public override bool Action(XmasMissionItemData target, Action onComplete) {
        var condition = CheckCondition(target);
        if (condition && action != null)
            action.Execute(target, onComplete);
        return condition;
    }

    public override bool CheckCondition(XmasMissionItemData mission) {
        if (IngameData.currentGameMode == GameMode.EventXmas) {
            return IngameHUD.Instance.Combat.GetTime() >= mission.PointTarget;
        }
        else
            return false;

    }

    public override bool CheckCondition(object target) {
        XmasMissionItemData convertData = (XmasMissionItemData)target;
        if (convertData != null && IngameData.currentGameMode == GameMode.EventXmas) {
            return IngameHUD.Instance.Combat.GetTime() >= convertData.PointTarget;
        }
        else
            return false;
    }
}
