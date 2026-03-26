
using Class_FSM;

public class MB05AppearCompleteTransition : MB05Transition {

    #region Singleton
    public MB05AppearCompleteTransition() {

    }
    private static MB05AppearCompleteTransition instance = null;
    public static MB05AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB05AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB05Base> controller) {
        bool isTransition = controller.ObjectBase.MB05Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB05IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB05Base> controller) {
    }
}
