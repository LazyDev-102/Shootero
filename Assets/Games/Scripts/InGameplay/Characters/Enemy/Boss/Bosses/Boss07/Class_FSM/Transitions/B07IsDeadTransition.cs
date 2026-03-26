

using Class_FSM;

public class B07IsDeadTransition : B07Transition {
    #region Singleton
    public B07IsDeadTransition() {

    }
    private static B07IsDeadTransition instance = null;
    public static B07IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new B07IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B07Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(B07DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B07Base> controller) {
    }
}
