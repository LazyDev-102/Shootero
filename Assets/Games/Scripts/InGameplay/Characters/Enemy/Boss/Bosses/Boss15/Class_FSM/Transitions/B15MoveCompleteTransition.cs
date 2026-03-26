

using Class_FSM;

public class B15MoveCompleteTransition : B15Transition {
    #region Singleton
    public B15MoveCompleteTransition() {

    }
    private static B15MoveCompleteTransition instance = null;
    public static B15MoveCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B15MoveCompleteTransition();
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
