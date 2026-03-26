using Class_FSM;

public class MB01ParentIsDeadTransition : MB01ParentTransition {

    #region Singleton
    public MB01ParentIsDeadTransition() {

    }
    private static MB01ParentIsDeadTransition instance = null;
    public static MB01ParentIsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentIsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB01ParentDeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01ParentBase> controller) {
    }
}
