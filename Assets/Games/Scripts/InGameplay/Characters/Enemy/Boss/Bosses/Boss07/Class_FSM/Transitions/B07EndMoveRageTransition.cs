

using Class_FSM;

public class B07EndMoveRageTransition : B07Transition {
    #region Singleton
    public B07EndMoveRageTransition() {

    }
    private static B07EndMoveRageTransition instance = null;
    public static B07EndMoveRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B07EndMoveRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B07Base> controller) {
        bool isTransition = controller.ObjectBase.B07Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B07RageState.Instance, this);
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
