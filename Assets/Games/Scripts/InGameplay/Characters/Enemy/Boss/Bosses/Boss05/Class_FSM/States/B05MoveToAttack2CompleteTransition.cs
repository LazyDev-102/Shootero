using Class_FSM;
using UnityEngine;

public class B05MoveToAttack2CompleteTransition : B05Transition {
    #region Singleton
    public B05MoveToAttack2CompleteTransition() {

    }
    private static B05MoveToAttack2CompleteTransition instance = null;
    public static B05MoveToAttack2CompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B05MoveToAttack2CompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B05Base> controller) {
        bool isTransition = controller.ObjectBase.B05Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B05Attack2State.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B05Base> controller) {
    }
}
