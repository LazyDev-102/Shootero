

using Class_FSM;

public class B10EndAppearTransition : B10Transition {
    #region Singleton
    public B10EndAppearTransition() {

    }
    private static B10EndAppearTransition instance = null;
    public static B10EndAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B10EndAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B10Base> controller) {
        bool isTransition = controller.ObjectBase.B10Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B10IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B10Base> controller) {
    }
}
