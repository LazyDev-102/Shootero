using Class_FSM;

public class HMB01ParentIsDeadTransition : HMB01ParentTransition {

    #region Singleton
    public HMB01ParentIsDeadTransition() {

    }
    private static HMB01ParentIsDeadTransition instance = null;
    public static HMB01ParentIsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentIsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(HMB01ParentDeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<HMB01ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<HMB01ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HMB01ParentBase> controller) {
    }
}
