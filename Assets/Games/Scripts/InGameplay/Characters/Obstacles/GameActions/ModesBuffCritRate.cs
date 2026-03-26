using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModesBuffCritRate", menuName = "Resource/GameAction/Modes/ModesBuffCritRate")]
public class ModesBuffCritRate : ModesAction {
    [SerializeField] private StatModifier stat;
    public override void Execute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.CritChance.AddModifier(stat);
    }

    public override void Execute(ObstacleBase target, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.CritChance.AddModifier(stat);
    }

    public override void RemoveExecute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.CritChance.RemoveModifier(stat);
    }
}
