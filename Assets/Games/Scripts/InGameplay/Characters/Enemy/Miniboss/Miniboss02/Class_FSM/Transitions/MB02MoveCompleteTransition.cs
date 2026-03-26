using Class_FSM;
using UnityEngine;

public class MB02MoveCompleteTransition : MB02Transition {

    #region Singleton
    public MB02MoveCompleteTransition() {

    }
    private static MB02MoveCompleteTransition instance = null;
    public static MB02MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB02MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB02Base> controller) {
        bool isTransition = controller.ObjectBase.MB02Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB02IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB02Base> controller) {
    }
}
