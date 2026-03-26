
using Class_FSM;

public class MB14AppearCompleteTransition : MB14Transition {

    #region Singleton
    public MB14AppearCompleteTransition() {

    }
    private static MB14AppearCompleteTransition instance = null;
    public static MB14AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB14AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB14Base> controller) {
        bool isTransition = controller.ObjectBase.MB14Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB14IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB14Base> controller) {
    }
}
