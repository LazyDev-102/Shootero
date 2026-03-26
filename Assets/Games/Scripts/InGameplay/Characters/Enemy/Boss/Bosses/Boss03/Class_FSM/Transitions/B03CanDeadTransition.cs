

using Class_FSM;

public class B03CanDeadTransition : B03Transition {
    #region Singleton
    public B03CanDeadTransition() {

    }
    private static B03CanDeadTransition instance = null;
    public static B03CanDeadTransition Instance {
        get {
            if(instance == null) {
                instance = new B03CanDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B03DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B03Base> controller) {
    }
}
