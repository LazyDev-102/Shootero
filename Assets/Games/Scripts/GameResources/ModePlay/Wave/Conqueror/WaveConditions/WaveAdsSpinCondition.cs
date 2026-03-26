using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveAdsSpinCondition", menuName = "Resource/Conditions/StartEndWave/WaveAdsSpinCondition")]
public class WaveAdsSpinCondition : WaveCondition<ShipBase> {
    public override bool Action(ShipBase target, Action onComplete) {
        var condition = CheckCondition(target);
        if (condition)
            action.Execute(target, onComplete);
        return condition;
    }

    public override bool CheckCondition(ShipBase ship) {
        return GameManager.Instance.GameLoader.Ship != null && ship.ShipHitbox.HitDamageInWave;
    }

    public override bool CheckCondition(object target) {
        return GameManager.Instance.GameLoader.Ship.ShipHitbox.HitDamageInWave && GameResources.Instance.AdsSpin.Spinable();
    }
}
