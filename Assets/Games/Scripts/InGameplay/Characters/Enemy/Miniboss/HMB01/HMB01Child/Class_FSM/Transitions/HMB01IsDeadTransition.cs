using Class_FSM;

public class HMB01IsDeadTransition : HMB01Transition {

    #region Singleton
    public HMB01IsDeadTransition() {

    }
    private static HMB01IsDeadTransition instance = null;
    public static HMB01IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(HMB01DeadState.Instance, this);
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
