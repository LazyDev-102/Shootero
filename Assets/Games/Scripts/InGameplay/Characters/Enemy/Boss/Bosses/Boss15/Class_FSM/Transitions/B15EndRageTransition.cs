

using Class_FSM;

public class B15EndRageTransition : B15Transition {

    #region Singleton
    public B15EndRageTransition() {

    }
    private static B15EndRageTransition instance = null;
    public static B15EndRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B15EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B15Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B15IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B15Base> controller) {
    }
}
