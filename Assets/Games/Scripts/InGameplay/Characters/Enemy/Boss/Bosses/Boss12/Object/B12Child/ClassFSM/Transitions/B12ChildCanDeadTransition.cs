

using Class_FSM;

public class B12ChildCanDeadTransition : B12ChildTransition {
    #region Singleton
    public B12ChildCanDeadTransition() {

    }
    private static B12ChildCanDeadTransition instance = null;
    public static B12ChildCanDeadTransition Instance {
        get {
            if(instance == null) {
                instance = new B12ChildCanDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12ChildBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B12ChildDeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B12ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12ChildBase> controller) {
    }
}
