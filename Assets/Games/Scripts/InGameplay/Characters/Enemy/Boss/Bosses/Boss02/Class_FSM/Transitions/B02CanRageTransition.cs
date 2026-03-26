

using Class_FSM;

public class B02CanRageTransition : B02Transition {
    #region Singleton
    public B02CanRageTransition() {

    }
    private static B02CanRageTransition instance = null;
    public static B02CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B02CanRageTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B02Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B02PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B02Base> controller) {
    }
}
