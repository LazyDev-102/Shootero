using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModesBuffAttackSpeed", menuName = "Resource/GameAction/Modes/ModesBuffAttackSpeed")]
public class ModesBuffAttackSpeed : ModesAction {
    [SerializeField] private StatModifier stat;
    public override void Execute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.AtkSpeed.AddModifier(stat);
    }

    public override void Execute(ObstacleBase target, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.AtkSpeed.AddModifier(stat);
    }

    public override void RemoveExecute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.AtkSpeed.RemoveModifier(stat);
    }
}
