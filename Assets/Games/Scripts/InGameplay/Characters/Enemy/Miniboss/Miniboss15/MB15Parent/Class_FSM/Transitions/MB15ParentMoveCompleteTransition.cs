using Class_FSM;
using UnityEngine;

public class MB15ParentMoveCompleteTransition : MB15ParentTransition {

    #region Singleton
    public MB15ParentMoveCompleteTransition() {

    }
    private static MB15ParentMoveCompleteTransition instance = null;
    public static MB15ParentMoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentMoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB15ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MB15ParentMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB15ParentIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ParentBase> controller) {
    }
}
