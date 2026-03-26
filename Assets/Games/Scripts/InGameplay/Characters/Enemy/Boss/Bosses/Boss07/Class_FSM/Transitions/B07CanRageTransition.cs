

using Class_FSM;

public class B07CanRageTransition : B07Transition {
    #region Singleton
    public B07CanRageTransition() {

    }
    private static B07CanRageTransition instance = null;
    public static B07CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B07CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B07Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B07PreRageState.Instance, this);
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
