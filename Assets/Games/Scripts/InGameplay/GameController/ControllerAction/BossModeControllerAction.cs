using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BossModeControllerAction", menuName = "Resource/GameAction/Controller/BossModeControllerAction")]
public class BossModeControllerAction : ControllerAction {
    public override GameController GetController(GameManager manager) {
        return new BossModeController(manager);
    }
    public override void Execute(GameManager target, object user, Action onCompleted) {
    }

    public override void Execute(GameManager target, Action onCompleted) {
    }

    public override void RemoveExecute(GameManager target, object user, Action onCompleted) {
    }
}
