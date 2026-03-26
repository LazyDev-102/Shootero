using Class_FSM;

public class MB02CanAppearTransition : MB02Transition {

    #region Singleton
    public MB02CanAppearTransition() {

    }
    private static MB02CanAppearTransition instance = null;
    public static MB02CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB02CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB02Base> controller) {
        bool isTransition = controller.ObjectBase.MB02Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB02AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB02Base> controller) {
    }
}
