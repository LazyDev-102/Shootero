using Class_FSM;
using UnityEngine;

public class MB12MoveCompleteTransition : MB12Transition {

    #region Singleton
    public MB12MoveCompleteTransition() {

    }
    private static MB12MoveCompleteTransition instance = null;
    public static MB12MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB12MoveCompleteTransition();
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
