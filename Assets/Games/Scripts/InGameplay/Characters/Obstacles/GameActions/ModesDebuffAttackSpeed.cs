using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModesDebuffAttackSpeed", menuName = "Resource/GameAction/Modes/ModesDebuffAttackSpeed")]
public class ModesDebuffAttackSpeed : ModesAction {
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
