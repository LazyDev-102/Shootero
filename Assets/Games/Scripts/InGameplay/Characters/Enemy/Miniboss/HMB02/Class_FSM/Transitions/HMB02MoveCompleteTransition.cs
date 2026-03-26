using Class_FSM;
using UnityEngine;

public class HMB02MoveCompleteTransition : HMB02Transition {

    #region Singleton
    public HMB02MoveCompleteTransition() {

    }
    private static HMB02MoveCompleteTransition instance = null;
    public static HMB02MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB02MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HMB02Base> controller) {
        bool isTransition = controller.ObjectBase.HMB02Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(HMB02IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<HMB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<HMB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HMB02Base> controller) {
    }
}
