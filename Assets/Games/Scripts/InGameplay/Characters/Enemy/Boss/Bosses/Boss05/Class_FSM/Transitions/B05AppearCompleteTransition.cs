

using Class_FSM;

public class B05AppearCompleteTransition : B05Transition {
    #region Singleton
    public B05AppearCompleteTransition() {

    }
    private static B05AppearCompleteTransition instance = null;
    public static B05AppearCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B05AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B05Base> controller) {
        bool isTransition = controller.ObjectBase.B05Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B05IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B05Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<B05Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<B05Base> controller) {

    }
}
