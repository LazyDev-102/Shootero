using Class_FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTurretState : TurretState
{
    #region Singleton
    public DeadTurretState() {

    }
    private static DeadTurretState instance = null;
    public static DeadTurretState Instance {
        get {
            if(instance == null) {
                instance = new DeadTurretState();
            }
            return instance;
        }
    }
    #endregion
    public override Color SceneGizmoColor => Color.gray;

    protected override Transition<TurretBase>[] GetTransitions() {
        return null;
    }

    protected override void DoStartActions(StateController<TurretBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<TurretBase> controller) {
    }

    protected override void DoEndActions(StateController<TurretBase> controller) {
    }
}
