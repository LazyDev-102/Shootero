using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GearModeControllerAction", menuName = "Resource/GameAction/Controller/GearModeControllerAction")]
public class GearModeControllerAction : ControllerAction {
    public override GameController GetController(GameManager manager) {
        return new GearModeController(manager);
    }
    public override void Execute(GameManager target, object user, Action onCompleted) {
    }

    public override void Execute(GameManager target, Action onCompleted) {
    }

    public override void RemoveExecute(GameManager target, object user, Action onCompleted) {
    }
}
