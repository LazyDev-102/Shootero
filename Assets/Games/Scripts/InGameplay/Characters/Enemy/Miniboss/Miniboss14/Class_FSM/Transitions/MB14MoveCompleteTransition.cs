using Class_FSM;
using UnityEngine;

public class MB14MoveCompleteTransition : MB14Transition {

    #region Singleton
    public MB14MoveCompleteTransition() {

    }
    private static MB14MoveCompleteTransition instance = null;
    public static MB14MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB14MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB14Base> controller) {
        bool isTransition = controller.ObjectBase.MB14Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB14IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB14Base> controller) {
    }
}
