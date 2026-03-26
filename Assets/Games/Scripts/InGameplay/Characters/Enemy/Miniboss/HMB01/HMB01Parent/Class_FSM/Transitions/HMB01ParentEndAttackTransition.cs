using Class_FSM;
using UnityEngine;

public class HMB01ParentEndAttackTransition : HMB01ParentTransition {

    #region Singleton
    public HMB01ParentEndAttackTransition() {

    }
    private static HMB01ParentEndAttackTransition instance = null;
    public static HMB01ParentEndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentEndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB01ParentBase> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(HMB01ParentMoveState.Instance, this);
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
