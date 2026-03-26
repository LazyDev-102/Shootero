using Class_FSM;

public class B14ChildIsDeadTransition : B14ChildTransition {

    #region Singleton
    public B14ChildIsDeadTransition() {

    }
    private static B14ChildIsDeadTransition instance = null;
    public static B14ChildIsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new B14ChildIsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B14ChildBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(B14ChildDeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14ChildBase> controller) {
    }
}
