

using Class_FSM;

public class B08CanRageTransition : B08Transition {
    #region Singleton
    public B08CanRageTransition() {

    }
    private static B08CanRageTransition instance = null;
    public static B08CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B08CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B08Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B08PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B08Base> controller) {
    }
}
