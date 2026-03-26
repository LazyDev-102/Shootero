using Class_FSM;

public class MB10CanAppearTransition : MB10Transition {

    #region Singleton
    public MB10CanAppearTransition() {

    }
    private static MB10CanAppearTransition instance = null;
    public static MB10CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB10CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB10Base> controller) {
        bool isTransition = controller.ObjectBase.MB10Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB10AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB10Base> controller) {
    }
}
