using Class_FSM;

public class MB12IsDeadTransition : MB12Transition {

    #region Singleton
    public MB12IsDeadTransition() {

    }
    private static MB12IsDeadTransition instance = null;
    public static MB12IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB12IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB12Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB12DeadState.Instance, this);
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
