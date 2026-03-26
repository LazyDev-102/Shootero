
using Class_FSM;

public class MB03AppearCompleteTransition : MB03Transition {

    #region Singleton
    public MB03AppearCompleteTransition() {

    }
    private static MB03AppearCompleteTransition instance = null;
    public static MB03AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB03AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB03Base> controller) {
        bool isTransition = controller.ObjectBase.MB03Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB03IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB03Base> controller) {
    }
}
