

using Class_FSM;

public class B04CanRageTransition : B04Transition {

    #region Singleton
    public B04CanRageTransition() {

    }
    private static B04CanRageTransition instance = null;
    public static B04CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B04CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B04Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B04PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B04Base> controller) {
    }
}
