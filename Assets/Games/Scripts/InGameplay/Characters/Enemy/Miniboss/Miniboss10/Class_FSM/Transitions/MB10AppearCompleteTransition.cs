using Class_FSM;

public class MB10AppearCompleteTransition : MB10Transition {

    #region Singleton
    public MB10AppearCompleteTransition() {

    }
    private static MB10AppearCompleteTransition instance = null;
    public static MB10AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB10AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB10Base> controller) {
        bool isTransition = controller.ObjectBase.MB10Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB10IdleState.Instance, this);
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
