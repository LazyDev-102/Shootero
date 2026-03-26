using Class_FSM;
using UnityEngine;

public class B14ChildMoveCompleteTransition : B14ChildTransition {

    #region Singleton
    public B14ChildMoveCompleteTransition() {

    }
    private static B14ChildMoveCompleteTransition instance = null;
    public static B14ChildMoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new B14ChildMoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B14ChildBase> controller) {
        bool isTransition = controller.ObjectBase.B14ChildMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B14ChildIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14ChildBase> controller) {
    }
}
