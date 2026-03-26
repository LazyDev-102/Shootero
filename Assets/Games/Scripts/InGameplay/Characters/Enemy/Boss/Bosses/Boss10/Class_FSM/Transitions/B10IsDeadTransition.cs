

using Class_FSM;

public class B10IsDeadTransition : B10Transition {
    #region Singleton
    public B10IsDeadTransition() {

    }
    private static B10IsDeadTransition instance = null;
    public static B10IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new B10IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B10Base> controller) {
        bool isTranstion = controller.ObjectBase.IsDie();
        if (isTranstion) {
            controller.TransitionToState(B10DeadState.Instance, this);
        }
        return isTranstion;
    }

    public override void DoAfterTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B10Base> controller) {
    }
}
