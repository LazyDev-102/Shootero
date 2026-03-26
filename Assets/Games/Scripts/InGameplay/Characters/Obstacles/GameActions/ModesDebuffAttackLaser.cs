using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ModesDebuffAttackLaser", menuName = "Resource/GameAction/Modes/ModesDebuffAttackLaser")]
public class ModesDebuffAttackLaser : ModesAction {
    [SerializeField] private StatModifier stat;
    [SerializeField] private float deltaTime;
    public override void Execute(ObstacleBase target, object user, Action onCompleted) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (!ship.ShipHitbox.ProtectShieldManager.gameObject.activeInHierarchy) {
            target.SetDurationWithUnlimitStat(deltaTime);
            ship.ShipHealth.AddHpByPercent(stat.Value);
        }
    }

    public override void Execute(ObstacleBase target, Action onCompleted) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (!ship.ShipHitbox.ProtectShieldManager.gameObject.activeInHierarchy) {
            target.SetDurationWithUnlimitStat(deltaTime);
            ship.ShipHealth.AddHpByPercent(stat.Value);
        }
    }

    public override void RemoveExecute(ObstacleBase target, object user, Action onCompleted) {
    }
}
