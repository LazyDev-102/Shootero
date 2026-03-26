

using Class_FSM;

public class B07EndMoveTransition : B07Transition {
    #region Singleton
    public B07EndMoveTransition() {

    }
    private static B07EndMoveTransition instance = null;
    public static B07EndMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new B07EndMoveTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B07Base> controller) {
        bool isTransition = controller.ObjectBase.B07Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B07IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B07Base> controller) {
    }
}
