using Class_FSM;
using UnityEngine;

public class MB10MoveState : MB10State {

    #region Singleton
    public MB10MoveState() {

    }
    private static MB10MoveState instance = null;
    public static MB10MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB10MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB10Transition[] transitions = { MB10MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB10Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB10Base> controller) {
        controller.ObjectBase.MB10Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB10Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB10Move.MoveDirect();
    }

    protected override Transition<MB10Base>[] GetTransitions() {
        return transitions;
    }
}
