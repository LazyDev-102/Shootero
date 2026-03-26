using Class_FSM;

public class B09RefectorCanRageTransition : B09RefectorTransition {

    #region Singleton
    public B09RefectorCanRageTransition() {

    }
    private static B09RefectorCanRageTransition instance = null;
    public static B09RefectorCanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorCanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09RefectorBase> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B09RefectorPreRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09RefectorBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09RefectorBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09RefectorBase> controller) {
    }
}
