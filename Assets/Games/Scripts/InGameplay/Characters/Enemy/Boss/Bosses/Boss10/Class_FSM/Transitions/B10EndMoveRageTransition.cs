using Class_FSM;
using UnityEngine;

public class B10EndMoveRageTransition : B10Transition {
    #region Singleton
    public B10EndMoveRageTransition() {

    }
    private static B10EndMoveRageTransition instance = null;
    public static B10EndMoveRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B10EndMoveRageTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B10Base> controller) {
        bool isTransition = controller.ObjectBase.B10Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B10AttackRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B10Base> controller) {
    }
}
