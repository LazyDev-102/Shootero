
using Class_FSM;

public class MB09AppearCompleteTransition : MB09Transition {

    #region Singleton
    public MB09AppearCompleteTransition() {

    }
    private static MB09AppearCompleteTransition instance = null;
    public static MB09AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB09Base> controller) {
        bool isTransition = controller.ObjectBase.MB09Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB09IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09Base> controller) {
    }
}
