using Class_FSM;

public class B05CanRageTransition : B05Transition {

    #region Singleton
    public B05CanRageTransition() {

    }
    private static B05CanRageTransition instance = null;
    public static B05CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B05CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B05Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B05PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B05Base> controller) {
    }
}
