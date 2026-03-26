using Class_FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTurretState : TurretState {
    #region Singleton
    public IdleTurretState() {

    }
    private static IdleTurretState instance = null;
    public static IdleTurretState Instance {
        get {
            if(instance == null) {
                instance = new IdleTurretState();
            }
            return instance;
        }
    }
    #endregion

    private Transition<TurretBase>[] transitions = {  };

    public override Color SceneGizmoColor => Color.green;

    protected override Transition<TurretBase>[] GetTransitions() {
        return transitions;
    }

    protected override void DoStartActions(StateController<TurretBase> controller) {
    }

    protected override void DoUpdateActions(StateController<TurretBase> controller) {
        controller.ObjectBase.TurretAttack.Attack();
    }

    protected override void DoEndActions(StateController<TurretBase> controller) {
    }
}
