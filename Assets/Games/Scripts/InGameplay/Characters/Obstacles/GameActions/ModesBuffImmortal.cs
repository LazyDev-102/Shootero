using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModesBuffImmortal", menuName = "Resource/GameAction/Modes/ModesBuffImmortal")]
public class ModesBuffImmortal : ModesAction {
    [SerializeField] private StatModifier stat;
    public override void Execute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipHitbox.TurnOnInvulnerable(-1);
    }

    public override void Execute(ObstacleBase target, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipHitbox.TurnOnInvulnerable(-1);
    }

    public override void RemoveExecute(ObstacleBase target, object user, Action onCompleted) {
        GameManager.Instance.GameLoader.Ship.ShipHitbox.TurnOffInvulnerable();
    }
}
