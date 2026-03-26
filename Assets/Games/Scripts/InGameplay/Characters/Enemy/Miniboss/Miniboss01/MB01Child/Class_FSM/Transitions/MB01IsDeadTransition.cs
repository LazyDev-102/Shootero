using Class_FSM;

public class MB01IsDeadTransition : MB01Transition {

    #region Singleton
    public MB01IsDeadTransition() {

    }
    private static MB01IsDeadTransition instance = null;
    public static MB01IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB01DeadState.Instance, this);
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
