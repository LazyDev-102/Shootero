using Class_FSM;
using UnityEngine;

public class MB10MoveCompleteTransition : MB10Transition {

    #region Singleton
    public MB10MoveCompleteTransition() {

    }
    private static MB10MoveCompleteTransition instance = null;
    public static MB10MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB10MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB10Base> controller) {
        bool isTransition = controller.ObjectBase.MB10Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB10IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB10Base> controller) {
    }
}
