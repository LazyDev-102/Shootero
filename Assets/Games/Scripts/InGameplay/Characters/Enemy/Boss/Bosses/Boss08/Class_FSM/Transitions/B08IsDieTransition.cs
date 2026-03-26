

using Class_FSM;

public class B08IsDieTransition : B08Transition {
    #region Singleton
    public B08IsDieTransition() {

    }
    private static B08IsDieTransition instance = null;
    public static B08IsDieTransition Instance {
        get {
            if (instance == null) {
                instance = new B08IsDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B08Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(B08DeadState.Instance, this);
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
