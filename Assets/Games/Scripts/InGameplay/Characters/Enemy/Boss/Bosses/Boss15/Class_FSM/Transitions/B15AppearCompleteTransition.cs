

using Class_FSM;

public class B15AppearCompleteTransition : B15Transition {
    #region Singleton
    public B15AppearCompleteTransition() {

    }
    private static B15AppearCompleteTransition instance = null;
    public static B15AppearCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B15AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B15Base> controller) {
        bool isTransition = controller.ObjectBase.B15Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B15IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B15Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<B15Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<B15Base> controller) {

    }
}
