using Class_FSM;
using UnityEngine;

public class MB17MoveCompleteTransition : MB17Transition {

    #region Singleton
    public MB17MoveCompleteTransition() {

    }
    private static MB17MoveCompleteTransition instance = null;
    public static MB17MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB17MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB17Base> controller) {
        bool isTransition = controller.ObjectBase.MB17Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB17IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB17Base> controller) {
    }
}
