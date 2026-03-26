using Class_FSM;

public class MB13IsDeadTransition : MB13Transition {

    #region Singleton
    public MB13IsDeadTransition() {

    }
    private static MB13IsDeadTransition instance = null;
    public static MB13IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB13IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB13Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB13DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB13Base> controller) {
    }
}
