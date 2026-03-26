using Class_FSM;
using UnityEngine;

public class MB08CanAttackTransition : MB08Transition {
    #region Singleton
    public MB08CanAttackTransition() {

    }
    private static MB08CanAttackTransition instance = null;
    public static MB08CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB08CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB08Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB08Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB08AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB08Base> controller) {
    }
}
