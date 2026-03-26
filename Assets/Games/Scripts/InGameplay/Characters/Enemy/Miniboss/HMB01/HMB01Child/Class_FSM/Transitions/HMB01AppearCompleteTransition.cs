
using Class_FSM;

public class HMB01AppearCompleteTransition : HMB01Transition {

    #region Singleton
    public HMB01AppearCompleteTransition() {

    }
    private static HMB01AppearCompleteTransition instance = null;
    public static HMB01AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HMB01Base> controller) {
        bool isTransition = controller.ObjectBase.HMB01Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(HMB01IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<HMB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<HMB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HMB01Base> controller) {
    }
}
