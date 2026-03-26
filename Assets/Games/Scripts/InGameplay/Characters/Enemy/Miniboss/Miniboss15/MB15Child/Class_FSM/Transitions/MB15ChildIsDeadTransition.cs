using Class_FSM;

public class MB15ChildIsDeadTransition : MB15ChildTransition {

    #region Singleton
    public MB15ChildIsDeadTransition() {

    }
    private static MB15ChildIsDeadTransition instance = null;
    public static MB15ChildIsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildIsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB15ChildBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB15ChildDeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ChildBase> controller) {
    }
}
