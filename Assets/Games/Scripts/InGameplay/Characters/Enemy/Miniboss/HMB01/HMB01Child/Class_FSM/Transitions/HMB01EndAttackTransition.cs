using Class_FSM;
using UnityEngine;

public class HMB01EndAttackTransition : HMB01Transition {

    #region Singleton
    public HMB01EndAttackTransition() {

    }
    private static HMB01EndAttackTransition instance = null;
    public static HMB01EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB01Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(HMB01MoveState.Instance, this);
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
