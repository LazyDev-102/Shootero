

using Class_FSM;

public class B12AppearCompleteTransition : B12Transition {
    #region Singleton
    public B12AppearCompleteTransition() {

    }
    private static B12AppearCompleteTransition instance = null;
    public static B12AppearCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B12AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12Base> controller) {
        bool isTransition = controller.ObjectBase.B12Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B12IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<B12Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<B12Base> controller) {

    }
}
