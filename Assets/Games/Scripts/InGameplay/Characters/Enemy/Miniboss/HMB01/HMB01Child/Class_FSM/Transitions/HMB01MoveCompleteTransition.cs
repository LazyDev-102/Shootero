using Class_FSM;
using UnityEngine;

public class HMB01MoveCompleteTransition : HMB01Transition {

    #region Singleton
    public HMB01MoveCompleteTransition() {

    }
    private static HMB01MoveCompleteTransition instance = null;
    public static HMB01MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HMB01Base> controller) {
        bool isTransition = controller.ObjectBase.HMB01Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(HMB01IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<HMB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<HMB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HMB01Base> controller) {
    }
}
