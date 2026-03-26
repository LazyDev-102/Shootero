using Class_FSM;
using UnityEngine;

public class MB16CanAttackTransition : MB16Transition {

    #region Singleton
    public MB16CanAttackTransition() {

    }
    private static MB16CanAttackTransition instance = null;
    public static MB16CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB16CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB16Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB16Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB16AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB16Base> controller) {
    }
}
