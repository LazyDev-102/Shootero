using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveSpaceMerchantCondition", menuName = "Resource/Conditions/StartEndWave/WaveSpaceMerchantCondition")]
public class WaveSpaceMerchantCondition : WaveCondition<ShipBase> {
    public override bool Action(ShipBase target, Action onComplete) {
        var condition = CheckCondition(target);
        if (condition)
            action.Execute(target, onComplete);
        return condition;
    }

    public override bool CheckCondition(ShipBase target) {
        return true;
    }

    public override bool CheckCondition(object target) {
        return true;
    }
}
