
using Class_FSM;

public class MB01AppearCompleteTransition : MB01Transition {

    #region Singleton
    public MB01AppearCompleteTransition() {

    }
    private static MB01AppearCompleteTransition instance = null;
    public static MB01AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB01Base> controller) {
        bool isTransition = controller.ObjectBase.MB01Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB01IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01Base> controller) {
    }
}
