using Class_FSM;

public class HMB01CanAppearTransition : HMB01Transition {

    #region Singleton
    public HMB01CanAppearTransition() {

    }
    private static HMB01CanAppearTransition instance = null;
    public static HMB01CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB01Base> controller) {
        bool isTransition = controller.ObjectBase.HMB01Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(HMB01AppearState.Instance, this);
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
