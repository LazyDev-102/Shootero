using Class_FSM;
using UnityEngine;

public class B12MoveToAttack2CompleteTransition : B12Transition {
    #region Singleton
    public B12MoveToAttack2CompleteTransition() {

    }
    private static B12MoveToAttack2CompleteTransition instance = null;
    public static B12MoveToAttack2CompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new B12MoveToAttack2CompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12Base> controller) {
        bool isTransition = controller.ObjectBase.B12Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B12Attack2State.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12Base> controller) {
    }
}
