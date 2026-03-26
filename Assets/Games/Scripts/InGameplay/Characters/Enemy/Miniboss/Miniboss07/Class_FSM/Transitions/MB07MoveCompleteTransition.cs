using Class_FSM;
using UnityEngine;

public class MB07MoveCompleteTransition : MB07Transition {

    #region Singleton
    public MB07MoveCompleteTransition() {

    }
    private static MB07MoveCompleteTransition instance = null;
    public static MB07MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB07MoveCompleteTransition();
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
