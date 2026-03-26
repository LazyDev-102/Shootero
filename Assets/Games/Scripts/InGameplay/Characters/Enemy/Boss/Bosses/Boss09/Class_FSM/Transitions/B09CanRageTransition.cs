using Class_FSM;

public class B09CanRageTransition : B09Transition {

    #region Singleton
    public B09CanRageTransition() {

    }
    private static B09CanRageTransition instance = null;
    public static B09CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B09CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B09PreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09Base> controller) {
    }
}
