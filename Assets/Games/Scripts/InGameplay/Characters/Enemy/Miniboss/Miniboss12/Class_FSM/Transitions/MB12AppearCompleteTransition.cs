
using Class_FSM;

public class MB12AppearCompleteTransition : MB12Transition {

    #region Singleton
    public MB12AppearCompleteTransition() {

    }
    private static MB12AppearCompleteTransition instance = null;
    public static MB12AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB12AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB12Base> controller) {
        bool isTransition = controller.ObjectBase.MB12Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB12IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB12Base> controller) {
    }
}
