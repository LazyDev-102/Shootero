
using Class_FSM;

public class MB02AppearCompleteTransition : MB02Transition {

    #region Singleton
    public MB02AppearCompleteTransition() {

    }
    private static MB02AppearCompleteTransition instance = null;
    public static MB02AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB02AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB02Base> controller) {
        bool isTransition = controller.ObjectBase.MB02Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB02IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB02Base> controller) {
    }
}
