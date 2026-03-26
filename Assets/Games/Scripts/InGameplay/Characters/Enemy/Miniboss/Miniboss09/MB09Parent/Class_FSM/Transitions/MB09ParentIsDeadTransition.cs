using Class_FSM;

public class MB09ParentIsDeadTransition : MB09ParentTransition {

    #region Singleton
    public MB09ParentIsDeadTransition() {

    }
    private static MB09ParentIsDeadTransition instance = null;
    public static MB09ParentIsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentIsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB09ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB09ParentDeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09ParentBase> controller) {
    }
}
