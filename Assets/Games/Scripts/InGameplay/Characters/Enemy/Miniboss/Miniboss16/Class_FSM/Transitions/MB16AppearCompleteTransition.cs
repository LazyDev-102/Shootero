
using Class_FSM;

public class MB16AppearCompleteTransition : MB16Transition {

    #region Singleton
    public MB16AppearCompleteTransition() {

    }
    private static MB16AppearCompleteTransition instance = null;
    public static MB16AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB16AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB16Base> controller) {
        bool isTransition = controller.ObjectBase.MB16Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB16IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB16Base> controller) {
    }
}
