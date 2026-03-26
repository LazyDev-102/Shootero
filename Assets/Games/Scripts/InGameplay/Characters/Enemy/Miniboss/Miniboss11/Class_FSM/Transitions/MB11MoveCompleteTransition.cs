using Class_FSM;
using UnityEngine;

public class MB11MoveCompleteTransition : MB11Transition {

    #region Singleton
    public MB11MoveCompleteTransition() {

    }
    private static MB11MoveCompleteTransition instance = null;
    public static MB11MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB11MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB11Base> controller) {
        bool isTransition = controller.ObjectBase.MB11Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB11IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB11Base> controller) {
    }
}
