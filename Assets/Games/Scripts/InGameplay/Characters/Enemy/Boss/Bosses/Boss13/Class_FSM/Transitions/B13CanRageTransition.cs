using Class_FSM;

public class B13CanRageTransition : B13Transition {

    #region Singleton
    public B13CanRageTransition() {

    }
    private static B13CanRageTransition instance = null;
    public static B13CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B13CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B13Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B13PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B13Base> controller) {
    }
}
