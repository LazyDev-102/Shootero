using Class_FSM;
using UnityEngine;

public class MB08MoveState : MB08State {

    #region Singleton
    public MB08MoveState() {

    }
    private static MB08MoveState instance = null;
    public static MB08MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB08MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB08Transition[] transitions = { MB08MoveCompleteTransition.Instance };
    protected override void DoEndActions(StateController<MB08Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB08Base> controller) {
        controller.ObjectBase.MB08Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<MB08Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB08Move.MoveDirect();
    }

    protected override Transition<MB08Base>[] GetTransitions() {
        return transitions;
    }
}
