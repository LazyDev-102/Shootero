using Class_FSM;
using UnityEngine;

public class B06MoveToAttack2CompleteTransition : B06Transition {
    #region Singleton
    public B06MoveToAttack2CompleteTransition() {

    }
    private static B06MoveToAttack2CompleteTransition instance = null;
    public static B06MoveToAttack2CompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B06MoveToAttack2CompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B06Base> controller) {
        bool isTransition = controller.ObjectBase.B06Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B06Attack2State.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B06Base> controller) {
    }
}
