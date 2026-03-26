using Class_FSM;

public class MB10IsDeadTransition : MB10Transition {

    #region Singleton
    public MB10IsDeadTransition() {

    }
    private static MB10IsDeadTransition instance = null;
    public static MB10IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB10IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB10Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB10DeadState.Instance, this);
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
