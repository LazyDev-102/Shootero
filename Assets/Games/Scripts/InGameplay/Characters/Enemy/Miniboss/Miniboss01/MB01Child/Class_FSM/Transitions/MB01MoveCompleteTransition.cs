using Class_FSM;
using UnityEngine;

public class MB01MoveCompleteTransition : MB01Transition {

    #region Singleton
    public MB01MoveCompleteTransition() {

    }
    private static MB01MoveCompleteTransition instance = null;
    public static MB01MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB01Base> controller) {
        bool isTransition = controller.ObjectBase.MB01Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB01IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01Base> controller) {
    }
}
