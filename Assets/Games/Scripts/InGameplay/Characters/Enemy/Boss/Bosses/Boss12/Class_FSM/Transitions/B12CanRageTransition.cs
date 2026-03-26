using Class_FSM;

public class B12CanRageTransition : B12Transition {

    #region Singleton
    public B12CanRageTransition() {

    }
    private static B12CanRageTransition instance = null;
    public static B12CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B12CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B12PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12Base> controller) {
    }
}
