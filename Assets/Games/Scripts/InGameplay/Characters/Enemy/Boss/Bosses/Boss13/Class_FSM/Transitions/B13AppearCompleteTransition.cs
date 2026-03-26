

using Class_FSM;

public class B13AppearCompleteTransition : B13Transition {
    #region Singleton
    public B13AppearCompleteTransition() {

    }
    private static B13AppearCompleteTransition instance = null;
    public static B13AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new B13AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B13Base> controller) {
        bool isTransition = controller.ObjectBase.B13Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B13IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B13Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<B13Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<B13Base> controller) {

    }
}
