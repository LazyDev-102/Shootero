
using Class_FSM;

public class MB17AppearCompleteTransition : MB17Transition {

    #region Singleton
    public MB17AppearCompleteTransition() {

    }
    private static MB17AppearCompleteTransition instance = null;
    public static MB17AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB17AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB17Base> controller) {
        bool isTransition = controller.ObjectBase.MB17Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB17IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB17Base> controller) {
    }
}
