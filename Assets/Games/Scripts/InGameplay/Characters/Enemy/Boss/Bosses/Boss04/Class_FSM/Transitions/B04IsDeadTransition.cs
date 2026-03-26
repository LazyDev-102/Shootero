

using Class_FSM;

public class B04IsDeadTransition : B04Transition {
    #region Singleton
    public B04IsDeadTransition() {

    }
    private static B04IsDeadTransition instance = null;
    public static B04IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new B04IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B04Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(B04DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B04Base> controller) {
    }
}
