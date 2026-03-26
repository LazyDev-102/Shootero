using Class_FSM;
using UnityEngine;

public class ME03B10EndMoveTransition : ME03B10Transition {

    #region Singleton
    public ME03B10EndMoveTransition() {

    }
    private static ME03B10EndMoveTransition instance = null;
    public static ME03B10EndMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new ME03B10EndMoveTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ME03B10Base> controller) {
        bool isTransition = controller.ObjectBase.ME03B10Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(ME03B10IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ME03B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ME03B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ME03B10Base> controller) {
    }
}
