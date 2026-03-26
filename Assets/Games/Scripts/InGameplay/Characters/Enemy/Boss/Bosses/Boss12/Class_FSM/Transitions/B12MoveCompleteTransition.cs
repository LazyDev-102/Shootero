

using Class_FSM;

public class B12MoveCompleteTransition : B12Transition {
    #region Singleton
    public B12MoveCompleteTransition() {

    }
    private static B12MoveCompleteTransition instance = null;
    public static B12MoveCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B12MoveCompleteTransition();
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
