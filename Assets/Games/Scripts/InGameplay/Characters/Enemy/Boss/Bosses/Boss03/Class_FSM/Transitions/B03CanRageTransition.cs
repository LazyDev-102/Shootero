

using Class_FSM;

public class B03CanRageTransition : B03Transition {
    #region Singleton
    public B03CanRageTransition() {

    }
    private static B03CanRageTransition instance = null;
    public static B03CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B03CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B03PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B03Base> controller) {
    }
}
