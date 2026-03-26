
using Class_FSM;

public class HMB01ParentAppearCompleteTransition : HMB01ParentTransition {

    #region Singleton
    public HMB01ParentAppearCompleteTransition() {

    }
    private static HMB01ParentAppearCompleteTransition instance = null;
    public static HMB01ParentAppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentAppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HMB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MinibossMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(HMB01ParentIdleState.Instance, this);
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
