using Class_FSM;
using UnityEngine;

public class HMB01ParentCanAttackTransition : HMB01ParentTransition {

    #region Singleton
    public HMB01ParentCanAttackTransition() {

    }
    private static HMB01ParentCanAttackTransition instance = null;
    public static HMB01ParentCanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentCanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<HMB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.HMB01ParentAttack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(HMB01ParentAttackState.Instance, this);
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
