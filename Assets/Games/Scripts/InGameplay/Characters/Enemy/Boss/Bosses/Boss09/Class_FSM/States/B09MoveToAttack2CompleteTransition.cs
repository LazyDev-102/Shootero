using Class_FSM;
using UnityEngine;

public class B09MoveToAttack2CompleteTransition : B09Transition {
    #region Singleton
    public B09MoveToAttack2CompleteTransition() {

    }
    private static B09MoveToAttack2CompleteTransition instance = null;
    public static B09MoveToAttack2CompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B09MoveToAttack2CompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09Base> controller) {
        bool isTransition = controller.ObjectBase.B09Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B09Attack2State.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09Base> controller) {
    }
}
