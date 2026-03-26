using Class_FSM;
using UnityEngine;

public class MB03MoveCompleteTransition : MB03Transition {

    #region Singleton
    public MB03MoveCompleteTransition() {

    }
    private static MB03MoveCompleteTransition instance = null;
    public static MB03MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB03MoveCompleteTransition();
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
