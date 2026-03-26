using Class_FSM;
using UnityEngine;

public class MB06MoveCompleteTransition : MB06Transition {

    #region Singleton
    public MB06MoveCompleteTransition() {

    }
    private static MB06MoveCompleteTransition instance = null;
    public static MB06MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new MB06MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB06Base> controller) {
        bool isTransition = controller.ObjectBase.MB06Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MB06IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB06Base> controller) {
    }
}
