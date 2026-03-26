
using Class_FSM;

public class B14ChildAppearCompleteTransition : B14ChildTransition {

    #region Singleton
    public B14ChildAppearCompleteTransition() {

    }
    private static B14ChildAppearCompleteTransition instance = null;
    public static B14ChildAppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new B14ChildAppearCompleteTransition();
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
