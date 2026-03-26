using Class_FSM;
using UnityEngine;

public class HMB01CanAttackTransition : HMB01Transition {

    #region Singleton
    public HMB01CanAttackTransition() {

    }
    private static HMB01CanAttackTransition instance = null;
    public static HMB01CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.HMB01Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(HMB01AttackState.Instance, this);
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
