using Class_FSM;
using UnityEngine;

public class MB05MoveCompleteTransition : MB05Transition {

    #region Singleton
    public MB05MoveCompleteTransition() {

    }
    private static MB05MoveCompleteTransition instance = null;
    public static MB05MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB05MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB05Base> controller) {
        bool isTransition = controller.ObjectBase.MB05Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB05IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB05Base> controller) {
    }
}
