using Class_FSM;
using UnityEngine;

public class B14MoveToAttack2CompleteTransition : B14Transition {
    #region Singleton
    public B14MoveToAttack2CompleteTransition() {

    }
    private static B14MoveToAttack2CompleteTransition instance = null;
    public static B14MoveToAttack2CompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new B14MoveToAttack2CompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B14Base> controller) {
        bool isTransition = controller.ObjectBase.B14Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B14Attack2State.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14Base> controller) {
    }
}
