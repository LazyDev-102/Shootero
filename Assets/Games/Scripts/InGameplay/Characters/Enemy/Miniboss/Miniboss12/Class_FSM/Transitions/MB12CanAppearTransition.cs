using Class_FSM;

public class MB12CanAppearTransition : MB12Transition {

    #region Singleton
    public MB12CanAppearTransition() {

    }
    private static MB12CanAppearTransition instance = null;
    public static MB12CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB12CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB12Base> controller) {
        bool isTransition = controller.ObjectBase.MB12Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB12AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB12Base> controller) {
    }
}
