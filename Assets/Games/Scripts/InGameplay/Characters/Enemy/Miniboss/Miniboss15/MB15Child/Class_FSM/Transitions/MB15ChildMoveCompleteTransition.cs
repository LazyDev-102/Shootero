using Class_FSM;
using UnityEngine;

public class MB15ChildMoveCompleteTransition : MB15ChildTransition {

    #region Singleton
    public MB15ChildMoveCompleteTransition() {

    }
    private static MB15ChildMoveCompleteTransition instance = null;
    public static MB15ChildMoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildMoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB15ChildBase> controller) {
        bool isTransition = controller.ObjectBase.MB15ChildMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB15ChildIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ChildBase> controller) {
    }
}
