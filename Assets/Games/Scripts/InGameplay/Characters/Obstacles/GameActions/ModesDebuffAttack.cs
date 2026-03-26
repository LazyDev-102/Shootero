using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModesDebuffAttack", menuName = "Resource/GameAction/Modes/ModesDebuffAttack")]
public class ModesDebuffAttack : ModesAction {
    [SerializeField] private StatModifier stat;
    public override void Execute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.Atk.AddModifier(stat);
    }

    public override void Execute(ObstacleBase target, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.Atk.AddModifier(stat);
    }

    public override void RemoveExecute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.Atk.RemoveModifier(stat);
    }
}
