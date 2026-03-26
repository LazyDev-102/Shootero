

using Class_FSM;

public class B14MoveCompleteTransition : B14Transition {
    #region Singleton
    public B14MoveCompleteTransition() {

    }
    private static B14MoveCompleteTransition instance = null;
    public static B14MoveCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B14MoveCompleteTransition();
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
