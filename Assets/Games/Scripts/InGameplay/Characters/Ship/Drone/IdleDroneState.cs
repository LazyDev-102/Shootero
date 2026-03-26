using Class_FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleDroneState : DroneState {
    #region Singleton
    public IdleDroneState() {

    }
    private static IdleDroneState instance = null;
    public static IdleDroneState Instance {
        get {
            if(instance == null) {
                instance = new IdleDroneState();
            }
            return instance;
        }
    }
    #endregion

    private Transition<DroneBase>[] transitions = {  };

    public override Color SceneGizmoColor => Color.green;

    protected override Transition<DroneBase>[] GetTransitions() {
        return transitions;
    }

    protected override void DoStartActions(StateController<DroneBase> controller) {
    }

    protected override void DoUpdateActions(StateController<DroneBase> controller) {
        controller.ObjectBase.DroneAttack.Attack();
    }

    protected override void DoEndActions(StateController<DroneBase> controller) {
    }
}
