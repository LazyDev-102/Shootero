using Class_FSM;

public class MB11IsDeadTransition : MB11Transition {

    #region Singleton
    public MB11IsDeadTransition() {

    }
    private static MB11IsDeadTransition instance = null;
    public static MB11IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB11IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB11Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB11DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB11Base> controller) {
    }
}
