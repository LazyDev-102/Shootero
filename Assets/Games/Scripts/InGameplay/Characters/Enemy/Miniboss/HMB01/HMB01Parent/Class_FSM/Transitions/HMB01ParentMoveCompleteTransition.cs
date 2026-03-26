using Class_FSM;
using UnityEngine;

public class HMB01ParentMoveCompleteTransition : HMB01ParentTransition {

    #region Singleton
    public HMB01ParentMoveCompleteTransition() {

    }
    private static HMB01ParentMoveCompleteTransition instance = null;
    public static HMB01ParentMoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentMoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HMB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MinibossMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(HMB01ParentIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<HMB01ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<HMB01ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HMB01ParentBase> controller) {
    }
}
