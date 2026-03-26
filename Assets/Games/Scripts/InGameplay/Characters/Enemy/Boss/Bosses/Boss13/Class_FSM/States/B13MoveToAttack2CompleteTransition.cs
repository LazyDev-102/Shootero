using Class_FSM;
using UnityEngine;

public class B13MoveToAttack2CompleteTransition : B13Transition {
    #region Singleton
    public B13MoveToAttack2CompleteTransition() {

    }
    private static B13MoveToAttack2CompleteTransition instance = null;
    public static B13MoveToAttack2CompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new B13MoveToAttack2CompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B13Base> controller) {
        bool isTransition = controller.ObjectBase.B13Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B13Attack2State.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B13Base> controller) {
    }
}
