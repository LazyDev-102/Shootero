using Class_FSM;

public class MB16CanAppearTransition : MB16Transition {

    #region Singleton
    public MB16CanAppearTransition() {

    }
    private static MB16CanAppearTransition instance = null;
    public static MB16CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB16CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB16Base> controller) {
        bool isTransition = controller.ObjectBase.MB16Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB16AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB16Base> controller) {
    }
}
