
using Class_FSM;

public class MB13AppearCompleteTransition : MB13Transition {

    #region Singleton
    public MB13AppearCompleteTransition() {

    }
    private static MB13AppearCompleteTransition instance = null;
    public static MB13AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB13AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB13Base> controller) {
        bool isTransition = controller.ObjectBase.MB13Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB13IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB13Base> controller) {
    }
}
