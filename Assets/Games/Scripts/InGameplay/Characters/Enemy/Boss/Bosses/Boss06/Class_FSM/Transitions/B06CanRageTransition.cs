using Class_FSM;

public class B06CanRageTransition : B06Transition {

    #region Singleton
    public B06CanRageTransition() {

    }
    private static B06CanRageTransition instance = null;
    public static B06CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B06CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B06Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B06PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B06Base> controller) {
    }
}
