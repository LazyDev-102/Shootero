using Class_FSM;
using UnityEngine;

public class MB09MoveState : MB09State {

    #region Singleton
    public MB09MoveState() {

    }
    private static MB09MoveState instance = null;
    public static MB09MoveState Instance {
        get {
            if (instance == null) {
                instance = new MB09MoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB09Transition[] transitions = { MB09MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB09Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB09Base> controller) {
        controller.ObjectBase.MB09Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB09Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB09Move.MoveDirect();
    }

    protected override Transition<MB09Base>[] GetTransitions() {
        return transitions;
    }
}
