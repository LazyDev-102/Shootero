using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModesBuffCritDamage", menuName = "Resource/GameAction/Modes/ModesBuffCritDamage")]
public class ModesBuffCritDamage : ModesAction {
    [SerializeField] private StatModifier critChanceModifier;
    [SerializeField] private StatModifier critDmgModifier;
    public override void Execute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.CritDamage.AddModifier(critDmgModifier);
        GameManager.Instance.GameLoader.Ship.ShipStat.CritChance.AddModifier(critChanceModifier);
    }

    public override void Execute(ObstacleBase target, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.CritDamage.AddModifier(critDmgModifier);
        GameManager.Instance.GameLoader.Ship.ShipStat.CritChance.AddModifier(critChanceModifier);
    }

    public override void RemoveExecute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipStat.CritDamage.RemoveModifier(critDmgModifier);
        GameManager.Instance.GameLoader.Ship.ShipStat.CritChance.RemoveModifier(critChanceModifier);
    }
}
