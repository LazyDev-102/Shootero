


using Class_FSM;

public class B10CanRageTransition : B10Transition {

    #region Singleton
    public B10CanRageTransition() {

    }
    private static B10CanRageTransition instance = null;
    public static B10CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B10CanRageTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B10Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B10PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B10Base> controller) {
    }
}
