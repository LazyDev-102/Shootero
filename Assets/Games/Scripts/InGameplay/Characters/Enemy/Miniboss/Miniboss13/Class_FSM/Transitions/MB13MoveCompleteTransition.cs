using Class_FSM;
using UnityEngine;

public class MB13MoveCompleteTransition : MB13Transition {

    #region Singleton
    public MB13MoveCompleteTransition() {

    }
    private static MB13MoveCompleteTransition instance = null;
    public static MB13MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB13MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB13Base> controller) {
        bool isTransition = controller.ObjectBase.MB13Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB13IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB13Base> controller) {
    }
}
