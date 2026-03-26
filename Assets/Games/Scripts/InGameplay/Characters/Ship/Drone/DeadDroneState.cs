using Class_FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadDroneState : DroneState {
    #region Singleton
    public DeadDroneState() {

    }
    private static DeadDroneState instance = null;
    public static DeadDroneState Instance {
        get {
            if (instance == null) {
                instance = new DeadDroneState();
            }
            return instance;
        }
    }
    #endregion
    public override Color SceneGizmoColor => Color.gray;
    private Transition<DroneBase>[] transitions = { DroneCanReviveTransition.Instance };

    protected override Transition<DroneBase>[] GetTransitions() {
        return transitions;
    }

    protected override void DoStartActions(StateController<DroneBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<DroneBase> controller) {
    }

    protected override void DoEndActions(StateController<DroneBase> controller) {
    }

}
