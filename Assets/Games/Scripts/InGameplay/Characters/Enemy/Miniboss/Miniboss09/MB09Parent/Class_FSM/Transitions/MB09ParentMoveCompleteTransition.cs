using Class_FSM;
using UnityEngine;

public class MB09ParentMoveCompleteTransition : MB09ParentTransition {

    #region Singleton
    public MB09ParentMoveCompleteTransition() {

    }
    private static MB09ParentMoveCompleteTransition instance = null;
    public static MB09ParentMoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentMoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB09ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MB09ParentMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB09ParentIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09ParentBase> controller) {
    }
}
