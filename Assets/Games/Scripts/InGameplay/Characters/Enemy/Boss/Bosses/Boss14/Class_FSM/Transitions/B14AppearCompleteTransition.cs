

using Class_FSM;

public class B14AppearCompleteTransition : B14Transition {
    #region Singleton
    public B14AppearCompleteTransition() {

    }
    private static B14AppearCompleteTransition instance = null;
    public static B14AppearCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B14AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B14Base> controller) {
        bool isTransition = controller.ObjectBase.B14Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B14IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<B14Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<B14Base> controller) {

    }
}
