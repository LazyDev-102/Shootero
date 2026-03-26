using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModesBuffHeal", menuName = "Resource/GameAction/Modes/ModesBuffHeal")]
public class ModesBuffHeal : ModesAction {
    [SerializeField] private StatModifier stat;
    [SerializeField] private float deltaTime;
    public override void Execute(ObstacleBase target, object user, Action onCompleted) {
        target.SetDurationWithUnlimitStat(deltaTime);
        GameManager.Instance.GameLoader.Ship.ShipHealth.AddHpByPercent(stat.Value);
    }

    public override void Execute(ObstacleBase target, Action onCompleted) {
        target.SetDurationWithUnlimitStat(deltaTime);
        GameManager.Instance.GameLoader.Ship.ShipHealth.AddHpByPercent(stat.Value);
    }

    public override void RemoveExecute(ObstacleBase target, object user, Action onCompleted) {
    }
}
