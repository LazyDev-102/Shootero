using Class_FSM;

public class MB14CanAppearTransition : MB14Transition {

    #region Singleton
    public MB14CanAppearTransition() {

    }
    private static MB14CanAppearTransition instance = null;
    public static MB14CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB14CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB14Base> controller) {
        bool isTransition = controller.ObjectBase.MB14Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB14AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB14Base> controller) {
    }
}
