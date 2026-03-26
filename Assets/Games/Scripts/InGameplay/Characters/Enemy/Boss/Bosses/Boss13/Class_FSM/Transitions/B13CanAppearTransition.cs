

using Class_FSM;

public class B13CanAppearTransition : B13Transition {
    #region Singleton
    public B13CanAppearTransition() {

    }
    private static B13CanAppearTransition instance = null;
    public static B13CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B13CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B13Base> controller) {
        bool isTransition = controller.ObjectBase.B13Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(B13AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B13Base> controller) {
    }
}
