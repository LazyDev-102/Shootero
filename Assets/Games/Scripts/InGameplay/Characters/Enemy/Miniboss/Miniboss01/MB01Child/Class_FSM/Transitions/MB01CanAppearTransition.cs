using Class_FSM;

public class MB01CanAppearTransition : MB01Transition {

    #region Singleton
    public MB01CanAppearTransition() {

    }
    private static MB01CanAppearTransition instance = null;
    public static MB01CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB01Base> controller) {
        bool isTransition = controller.ObjectBase.MB01Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB01AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01Base> controller) {
    }
}
