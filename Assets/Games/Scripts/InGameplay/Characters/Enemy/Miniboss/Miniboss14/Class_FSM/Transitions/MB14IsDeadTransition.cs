using Class_FSM;

public class MB14IsDeadTransition : MB14Transition {

    #region Singleton
    public MB14IsDeadTransition() {

    }
    private static MB14IsDeadTransition instance = null;
    public static MB14IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB14IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB14Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB14DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB14Base> controller) {
    }
}
