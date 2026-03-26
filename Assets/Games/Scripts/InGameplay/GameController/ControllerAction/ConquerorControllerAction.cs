using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConquerorControllerAction", menuName = "Resource/GameAction/Controller/ConquerorControllerAction")]
public class ConquerorControllerAction : ControllerAction {
    public override GameController GetController(GameManager manager) {
        return new ConquerorController(manager);
    }
    public override void Execute(GameManager target, object user, Action onCompleted) {
    }

    public override void Execute(GameManager target, Action onCompleted) {
    }

    public override void RemoveExecute(GameManager target, object user, Action onCompleted) {
    }
}
