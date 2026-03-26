using Class_FSM;

public class MB15ParentIsDeadTransition : MB15ParentTransition {

    #region Singleton
    public MB15ParentIsDeadTransition() {

    }
    private static MB15ParentIsDeadTransition instance = null;
    public static MB15ParentIsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentIsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB15ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB15ParentDeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ParentBase> controller) {
    }
}
