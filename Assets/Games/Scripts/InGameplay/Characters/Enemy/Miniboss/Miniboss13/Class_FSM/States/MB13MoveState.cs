using Class_FSM;
using UnityEngine;

public class MB13MoveState : MB13State {

    #region Singleton
    public MB13MoveState() {

    }
    private static MB13MoveState instance = null;
    public static MB13MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB13MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB13Transition[] transitions = { MB13MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB13Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB13Base> controller) {
        controller.ObjectBase.MB13Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB13Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB13Move.MoveDirect();
    }

    protected override Transition<MB13Base>[] GetTransitions() {
        return transitions;
    }
}
