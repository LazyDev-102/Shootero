
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "XmasModeControllerAction", menuName = "Resource/GameAction/Controller/XmasModeControllerAction")]
public class XmasModeControllerAction : ControllerAction {
    public override GameController GetController(GameManager manager) {
        return new XmasModeController(manager);
    }
    public override void Execute(GameManager target, object user, Action onCompleted) {
    }

    public override void Execute(GameManager target, Action onCompleted) {
    }

    public override void RemoveExecute(GameManager target, object user, Action onCompleted) {
    }
}
