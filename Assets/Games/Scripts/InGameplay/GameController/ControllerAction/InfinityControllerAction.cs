using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InfinityControllerAction", menuName = "Resource/GameAction/Controller/InfinityControllerAction")]
public class InfinityControllerAction : ControllerAction {
    public override GameController GetController(GameManager manager) {
        return new InfinityController(manager);
    }
    public override void Execute(GameManager target, object user, Action onCompleted) {
    }

    public override void Execute(GameManager target, Action onCompleted) {
    }

    public override void RemoveExecute(GameManager target, object user, Action onCompleted) {
    }
}
