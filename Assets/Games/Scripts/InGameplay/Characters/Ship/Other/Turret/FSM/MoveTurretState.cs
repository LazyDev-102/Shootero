using Class_FSM;
using UnityEngine;

public class MoveTurretState : TurretState
{
    #region Singleton
    private MoveTurretState() {

    }
    private static MoveTurretState instance = null;
    public static MoveTurretState Instance {
        get {
            if(instance == null) {
                instance = new MoveTurretState();
            }
            return instance;
        }
    }
    #endregion
    public override Color SceneGizmoColor => Color.cyan;

    private Transition<TurretBase>[] transitions = {  };

    protected override void DoEndActions(StateController<TurretBase> controller) {
    }

    protected override void DoStartActions(StateController<TurretBase> controller) {

    }

    protected override void DoUpdateActions(StateController<TurretBase> controller) {
        //Attack
        controller.ObjectBase.TurretAttack.Attack();
        // move controll
        //controller.ObjectBase.TurretMove.MoveControl();
    }

    protected override Transition<TurretBase>[] GetTransitions() {
        return transitions;
    }
}
