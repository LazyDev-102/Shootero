
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenModeControllerAction", menuName = "Resource/GameAction/Controller/HalloweenModeControllerAction")]
public class HalloweenModeControllerAction : ControllerAction {
    public override GameController GetController(GameManager manager) {
        return new HalloweenModeController(manager);
    }
    public override void Execute(GameManager target, object user, Action onCompleted) {
    }

    public override void Execute(GameManager target, Action onCompleted) {
    }

    public override void RemoveExecute(GameManager target, object user, Action onCompleted) {
    }
}
