using Class_FSM;
using UnityEngine;

public class MB01ParentMoveCompleteTransition : MB01ParentTransition {

    #region Singleton
    public MB01ParentMoveCompleteTransition() {

    }
    private static MB01ParentMoveCompleteTransition instance = null;
    public static MB01ParentMoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentMoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MB01ParentMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB01ParentIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01ParentBase> controller) {
    }
}
