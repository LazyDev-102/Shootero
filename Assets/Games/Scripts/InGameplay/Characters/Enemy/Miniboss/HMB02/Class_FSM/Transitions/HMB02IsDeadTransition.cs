using Class_FSM;

public class HMB02IsDeadTransition : HMB02Transition {

    #region Singleton
    public HMB02IsDeadTransition() {

    }
    private static HMB02IsDeadTransition instance = null;
    public static HMB02IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB02IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB02Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(HMB02DeadState.Instance, this);
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
