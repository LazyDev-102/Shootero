using Class_FSM;

public class MB03IsDeadTransition : MB03Transition {

    #region Singleton
    public MB03IsDeadTransition() {

    }
    private static MB03IsDeadTransition instance = null;
    public static MB03IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB03IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB03Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB03DeadState.Instance, this);
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
