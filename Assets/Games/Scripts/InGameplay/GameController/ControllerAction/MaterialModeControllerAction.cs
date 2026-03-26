
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialModeControllerAction", menuName = "Resource/GameAction/Controller/MaterialModeControllerAction")]
public class MaterialModeControllerAction : ControllerAction {
    public override GameController GetController(GameManager manager) {
        return new MaterialModeController(manager);
    }
    public override void Execute(GameManager target, object user, Action onCompleted) {
    }

    public override void Execute(GameManager target, Action onCompleted) {
    }

    public override void RemoveExecute(GameManager target, object user, Action onCompleted) {
    }
}
