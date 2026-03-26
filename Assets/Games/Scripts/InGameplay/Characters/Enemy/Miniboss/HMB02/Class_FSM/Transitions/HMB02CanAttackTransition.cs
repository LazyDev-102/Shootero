using Class_FSM;
using UnityEngine;

public class HMB02CanAttackTransition : HMB02Transition {

    #region Singleton
    public HMB02CanAttackTransition() {

    }
    private static HMB02CanAttackTransition instance = null;
    public static HMB02CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB02CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB02Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.HMB02Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(HMB02AttackState.Instance, this);
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
