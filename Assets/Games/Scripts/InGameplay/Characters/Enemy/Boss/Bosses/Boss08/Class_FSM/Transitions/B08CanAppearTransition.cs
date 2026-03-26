

using Class_FSM;

public class B08CanAppearTransition : B08Transition {
    #region Singleton
    public B08CanAppearTransition() {

    }
    private static B08CanAppearTransition instance = null;
    public static B08CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B08CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B08Base> controller) {
        bool isTransition = controller.ObjectBase.B08Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(B08AppearState.Instance, this);
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
