using Class_FSM;
using UnityEngine;

public class MB04MoveCompleteTransition : MB04Transition {

    #region Singleton
    public MB04MoveCompleteTransition() {

    }
    private static MB04MoveCompleteTransition instance = null;
    public static MB04MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB04MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB04Base> controller) {
        bool isTransition = controller.ObjectBase.MB04Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB04IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB04Base> controller) {
    }
}
