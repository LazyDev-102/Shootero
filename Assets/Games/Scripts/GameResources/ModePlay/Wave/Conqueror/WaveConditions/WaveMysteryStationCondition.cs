using UnityEngine;

[CreateAssetMenu(fileName = "WaveMysteryStationCondition", menuName = "Resource/Conditions/StartEndWave/WaveMysteryStationCondition")]
public class WaveMysteryStationCondition : WaveCondition<ShipBase> {
    public override bool Action(ShipBase target, System.Action onCompleted) {
        var condition = CheckCondition(target);
        if (condition)
            action.Execute(target, onCompleted);
        return condition;
    }
    public override bool CheckCondition(ShipBase ship) {
        return ship != null && !ship.ShipHitbox.HitDamageInWave && GameResources.Instance.MysteryStation.Tradeable();
    }

    public override bool CheckCondition(object target) {
        return !GameManager.Instance.GameLoader.Ship.ShipHitbox.HitDamageInWave && GameResources.Instance.MysteryStation.Tradeable();
    }
}
