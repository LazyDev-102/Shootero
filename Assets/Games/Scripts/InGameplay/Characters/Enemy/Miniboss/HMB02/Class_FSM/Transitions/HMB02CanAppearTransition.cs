using Class_FSM;

public class HMB02CanAppearTransition : HMB02Transition {

    #region Singleton
    public HMB02CanAppearTransition() {

    }
    private static HMB02CanAppearTransition instance = null;
    public static HMB02CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB02CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB02Base> controller) {
        bool isTransition = controller.ObjectBase.HMB02Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(HMB02AppearState.Instance, this);
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
