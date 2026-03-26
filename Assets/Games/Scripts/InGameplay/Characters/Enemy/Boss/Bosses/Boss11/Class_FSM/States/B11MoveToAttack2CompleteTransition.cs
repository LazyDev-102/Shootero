using Class_FSM;
using UnityEngine;

public class B11MoveToAttack2CompleteTransition : B11Transition {
    #region Singleton
    public B11MoveToAttack2CompleteTransition() {

    }
    private static B11MoveToAttack2CompleteTransition instance = null;
    public static B11MoveToAttack2CompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new B11MoveToAttack2CompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B11Base> controller) {
        bool isTransition = controller.ObjectBase.B11Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B11Attack2State.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B11Base> controller) {
    }
}
