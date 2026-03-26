using Class_FSM;
using UnityEngine;

public class MB17CanAttackTransition : MB17Transition {

    #region Singleton
    public MB17CanAttackTransition() {

    }
    private static MB17CanAttackTransition instance = null;
    public static MB17CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB17CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB17Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB17Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB17AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB17Base> controller) {
    }
}
