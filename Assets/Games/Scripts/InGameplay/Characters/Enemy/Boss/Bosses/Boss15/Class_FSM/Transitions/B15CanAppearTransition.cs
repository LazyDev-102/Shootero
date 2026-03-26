

using Class_FSM;

public class B15CanAppearTransition : B15Transition {
    #region Singleton
    public B15CanAppearTransition() {

    }
    private static B15CanAppearTransition instance = null;
    public static B15CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B15CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B15Base> controller) {
        bool isTransition = controller.ObjectBase.B15Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(B15AppearState.Instance, this);
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
