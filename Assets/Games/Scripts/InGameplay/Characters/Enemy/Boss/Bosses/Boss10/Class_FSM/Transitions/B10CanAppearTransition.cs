

using Class_FSM;

public class B10CanAppearTransition : B10Transition {
    #region Singleton
    public B10CanAppearTransition() {

    }
    private static B10CanAppearTransition instance = null;
    public static B10CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B10CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B10Base> controller) {
        bool isTransition = controller.ObjectBase.B10Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(B10AppearState.Instance, this);
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
