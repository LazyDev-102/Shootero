
using Class_FSM;

public class MB15ParentAppearCompleteTransition : MB15ParentTransition {

    #region Singleton
    public MB15ParentAppearCompleteTransition() {

    }
    private static MB15ParentAppearCompleteTransition instance = null;
    public static MB15ParentAppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentAppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB15ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MB15ParentMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB15ParentIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ParentBase> controller) {
    }
}
