using Class_FSM;
using UnityEngine;

public class MB16MoveCompleteTransition : MB16Transition {

    #region Singleton
    public MB16MoveCompleteTransition() {

    }
    private static MB16MoveCompleteTransition instance = null;
    public static MB16MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB16MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB16Base> controller) {
        bool isTransition = controller.ObjectBase.MB16Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB16IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB16Base> controller) {
    }
}
