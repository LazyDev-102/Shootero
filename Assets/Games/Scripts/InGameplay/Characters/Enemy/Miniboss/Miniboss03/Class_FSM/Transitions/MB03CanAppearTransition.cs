using Class_FSM;

public class MB03CanAppearTransition : MB03Transition {

    #region Singleton
    public MB03CanAppearTransition() {

    }
    private static MB03CanAppearTransition instance = null;
    public static MB03CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB03CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB03Base> controller) {
        bool isTransition = controller.ObjectBase.MB03Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB03AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB03Base> controller) {
    }
}
