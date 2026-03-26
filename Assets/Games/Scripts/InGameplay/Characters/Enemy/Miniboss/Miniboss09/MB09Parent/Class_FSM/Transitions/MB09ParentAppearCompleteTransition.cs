
using Class_FSM;

public class MB09ParentAppearCompleteTransition : MB09ParentTransition {

    #region Singleton
    public MB09ParentAppearCompleteTransition() {

    }
    private static MB09ParentAppearCompleteTransition instance = null;
    public static MB09ParentAppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentAppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB09ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MB09ParentMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB09ParentIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09ParentBase> controller) {
    }
}
