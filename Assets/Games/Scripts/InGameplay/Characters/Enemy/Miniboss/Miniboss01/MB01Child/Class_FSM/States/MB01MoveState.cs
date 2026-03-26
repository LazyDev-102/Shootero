using Class_FSM;
using UnityEngine;

public class MB01MoveState : MB01State {

    #region Singleton
    public MB01MoveState() {

    }
    private static MB01MoveState instance = null;
    public static MB01MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB01MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB01Transition[] transitions = { MB01MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB01Base> controller) {
        controller.ObjectBase.MB01Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB01Move.MoveDirect();
    }

    protected override Transition<MB01Base>[] GetTransitions() {
        return transitions;
    }
}
