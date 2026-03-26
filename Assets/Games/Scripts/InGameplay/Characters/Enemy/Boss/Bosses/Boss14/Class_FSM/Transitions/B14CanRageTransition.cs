using Class_FSM;

public class B14CanRageTransition : B14Transition {

    #region Singleton
    public B14CanRageTransition() {

    }
    private static B14CanRageTransition instance = null;
    public static B14CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B14CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B14Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B14PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14Base> controller) {
    }
}
