using Class_FSM;

public class B11CanRageTransition : B11Transition {

    #region Singleton
    public B11CanRageTransition() {

    }
    private static B11CanRageTransition instance = null;
    public static B11CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B11CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B11Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B11PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B11Base> controller) {
    }
}
