using Class_FSM;
using UnityEngine;

public class MB09MoveCompleteTransition : MB09Transition {

    #region Singleton
    public MB09MoveCompleteTransition() {

    }
    private static MB09MoveCompleteTransition instance = null;
    public static MB09MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB09Base> controller) {
        bool isTransition = controller.ObjectBase.MB09Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB09IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09Base> controller) {
    }
}
