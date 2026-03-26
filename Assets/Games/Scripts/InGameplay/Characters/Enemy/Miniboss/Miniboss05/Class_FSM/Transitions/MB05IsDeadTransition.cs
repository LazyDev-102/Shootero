using Class_FSM;

public class MB05IsDeadTransition : MB05Transition {

    #region Singleton
    public MB05IsDeadTransition() {

    }
    private static MB05IsDeadTransition instance = null;
    public static MB05IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB05IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB05Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB05DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB05Base> controller) {
    }
}
