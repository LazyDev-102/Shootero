using Class_FSM;

public class MB05CanAppearTransition : MB05Transition {

    #region Singleton
    public MB05CanAppearTransition() {

    }
    private static MB05CanAppearTransition instance = null;
    public static MB05CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB05CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB05Base> controller) {
        bool isTransition = controller.ObjectBase.MB05Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB05AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB05Base> controller) {
    }
}
