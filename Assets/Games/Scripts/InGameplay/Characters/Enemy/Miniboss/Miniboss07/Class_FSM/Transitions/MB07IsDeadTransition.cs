using Class_FSM;

public class MB07IsDeadTransition : MB07Transition {

    #region Singleton
    public MB07IsDeadTransition() {

    }
    private static MB07IsDeadTransition instance = null;
    public static MB07IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB07IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB07Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB07DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB07Base> controller) {
    }
}
