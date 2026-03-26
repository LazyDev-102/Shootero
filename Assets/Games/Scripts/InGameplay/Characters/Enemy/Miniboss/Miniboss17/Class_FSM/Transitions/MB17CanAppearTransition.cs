using Class_FSM;

public class MB17CanAppearTransition : MB17Transition {

    #region Singleton
    public MB17CanAppearTransition() {

    }
    private static MB17CanAppearTransition instance = null;
    public static MB17CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB17CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB17Base> controller) {
        bool isTransition = controller.ObjectBase.MB17Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB17AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB17Base> controller) {
    }
}
