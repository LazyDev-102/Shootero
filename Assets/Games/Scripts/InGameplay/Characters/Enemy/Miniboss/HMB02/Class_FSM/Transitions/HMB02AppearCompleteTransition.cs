using Class_FSM;

public class HMB02AppearCompleteTransition : HMB02Transition {

    #region Singleton
    public HMB02AppearCompleteTransition() {

    }
    private static HMB02AppearCompleteTransition instance = null;
    public static HMB02AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB02AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HMB02Base> controller) {
        bool isTransition = controller.ObjectBase.HMB02Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(HMB02IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<HMB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<HMB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HMB02Base> controller) {
    }
}
