
using Class_FSM;

public class MB15ChildAppearCompleteTransition : MB15ChildTransition {

    #region Singleton
    public MB15ChildAppearCompleteTransition() {

    }
    private static MB15ChildAppearCompleteTransition instance = null;
    public static MB15ChildAppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildAppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB15ChildBase> controller) {
        bool isTransition = controller.ObjectBase.MB15ChildMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB15ChildIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ChildBase> controller) {
    }
}
