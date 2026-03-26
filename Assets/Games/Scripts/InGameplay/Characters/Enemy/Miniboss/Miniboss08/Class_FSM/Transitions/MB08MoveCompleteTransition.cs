using Class_FSM;
using UnityEngine;

public class MB08MoveCompleteTransition : MB08Transition {

    #region Singleton
    public MB08MoveCompleteTransition() {

    }
    private static MB08MoveCompleteTransition instance = null;
    public static MB08MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB08MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB08Base> controller) {
        bool isTransition = controller.ObjectBase.MB08Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB08IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB08Base> controller) {
    }
}
