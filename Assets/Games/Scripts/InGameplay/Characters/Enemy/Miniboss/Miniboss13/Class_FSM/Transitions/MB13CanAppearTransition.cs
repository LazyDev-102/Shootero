using Class_FSM;

public class MB13CanAppearTransition : MB13Transition {

    #region Singleton
    public MB13CanAppearTransition() {

    }
    private static MB13CanAppearTransition instance = null;
    public static MB13CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB13CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB13Base> controller) {
        bool isTransition = controller.ObjectBase.MB13Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB13AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB13Base> controller) {
    }
}
