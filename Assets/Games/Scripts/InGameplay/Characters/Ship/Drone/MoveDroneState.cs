using Class_FSM;
using UnityEngine;

public class MoveDroneState : DroneState
{
    #region Singleton
    private MoveDroneState() {

    }
    private static MoveDroneState instance = null;
    public static MoveDroneState Instance {
        get {
            if(instance == null) {
                instance = new MoveDroneState();
            }
            return instance;
        }
    }
    #endregion
    public override Color SceneGizmoColor => Color.cyan;

    private Transition<DroneBase>[] transitions = {  };

    protected override void DoEndActions(StateController<DroneBase> controller) {
    }

    protected override void DoStartActions(StateController<DroneBase> controller) {

    }

    protected override void DoUpdateActions(StateController<DroneBase> controller) {
        //Attack
        controller.ObjectBase.DroneAttack.Attack();
        // move controll
        //controller.ObjectBase.DroneMove.MoveControl();
    }

    protected override Transition<DroneBase>[] GetTransitions() {
        return transitions;
    }
}
