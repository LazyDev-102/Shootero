using Class_FSM;
using UnityEngine;

public class MB14CanAttackTransition : MB14Transition {

    #region Singleton
    public MB14CanAttackTransition() {

    }
    private static MB14CanAttackTransition instance = null;
    public static MB14CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB14CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB14Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB14Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB14AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB14Base> controller) {
    }
}
