using Class_FSM;

public class MB16IsDeadTransition : MB16Transition {

    #region Singleton
    public MB16IsDeadTransition() {

    }
    private static MB16IsDeadTransition instance = null;
    public static MB16IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB16IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB16Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB16DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB16Base> controller) {
    }
}
