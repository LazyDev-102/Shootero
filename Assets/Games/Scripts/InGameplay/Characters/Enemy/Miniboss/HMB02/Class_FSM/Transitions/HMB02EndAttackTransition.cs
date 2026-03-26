using Class_FSM;
using UnityEngine;

public class HMB02EndAttackTransition : HMB02Transition {

    #region Singleton
    public HMB02EndAttackTransition() {

    }
    private static HMB02EndAttackTransition instance = null;
    public static HMB02EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB02EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB02Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(HMB02MoveState.Instance, this);
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
