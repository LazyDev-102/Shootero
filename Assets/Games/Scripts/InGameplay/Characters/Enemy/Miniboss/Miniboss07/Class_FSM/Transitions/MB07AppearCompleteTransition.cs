
using Class_FSM;

public class MB07AppearCompleteTransition : MB07Transition {

    #region Singleton
    public MB07AppearCompleteTransition() {

    }
    private static MB07AppearCompleteTransition instance = null;
    public static MB07AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB07AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB07Base> controller) {
        bool isTransition = controller.ObjectBase.MB07Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB07IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB07Base> controller) {
    }
}
