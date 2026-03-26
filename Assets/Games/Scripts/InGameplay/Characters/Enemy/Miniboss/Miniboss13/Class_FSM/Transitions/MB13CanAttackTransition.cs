using Class_FSM;
using UnityEngine;

public class MB13CanAttackTransition : MB13Transition {

    #region Singleton
    public MB13CanAttackTransition() {

    }
    private static MB13CanAttackTransition instance = null;
    public static MB13CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB13CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB13Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB13Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB13AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB13Base> controller) {
    }
}
