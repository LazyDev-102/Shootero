
using Class_FSM;

public class MB01ParentAppearCompleteTransition : MB01ParentTransition {

    #region Singleton
    public MB01ParentAppearCompleteTransition() {

    }
    private static MB01ParentAppearCompleteTransition instance = null;
    public static MB01ParentAppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentAppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MB01ParentMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB01ParentIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01ParentBase> controller) {
    }
}
