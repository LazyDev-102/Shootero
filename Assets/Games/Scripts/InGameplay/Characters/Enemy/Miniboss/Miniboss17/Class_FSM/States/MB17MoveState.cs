using Class_FSM;
using UnityEngine;

public class MB17MoveState : MB17State {

    #region Singleton
    public MB17MoveState() {

    }
    private static MB17MoveState instance = null;
    public static MB17MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB17MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB17Transition[] transitions = { MB17MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB17Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB17Base> controller) {
        controller.ObjectBase.MB17Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB17Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB17Move.MoveDirect();
    }

    protected override Transition<MB17Base>[] GetTransitions() {
        return transitions;
    }
}
