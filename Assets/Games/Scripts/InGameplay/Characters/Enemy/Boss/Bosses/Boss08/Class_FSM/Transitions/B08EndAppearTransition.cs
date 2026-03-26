

using Class_FSM;

public class B08EndAppearTransition : B08Transition {
    #region Singleton
    public B08EndAppearTransition() {

    }
    private static B08EndAppearTransition instance = null;
    public static B08EndAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B08EndAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B08Base> controller) {
        bool isTransition = controller.ObjectBase.B08Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B08IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B08Base> controller) {
    }
}
