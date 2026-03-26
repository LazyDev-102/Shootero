using Class_FSM;

public class HMB01ParentCanAppearTransition : HMB01ParentTransition {

    #region Singleton
    public HMB01ParentCanAppearTransition() {

    }
    private static HMB01ParentCanAppearTransition instance = null;
    public static HMB01ParentCanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentCanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MinibossMove.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(HMB01ParentAppearState.Instance, this);
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
