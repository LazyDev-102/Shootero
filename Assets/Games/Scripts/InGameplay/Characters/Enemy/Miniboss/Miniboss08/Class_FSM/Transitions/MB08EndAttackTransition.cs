using Class_FSM;
using UnityEngine;

public class MB08EndAttackTransition : MB08Transition {

    #region Singleton
    public MB08EndAttackTransition() {

    }
    private static MB08EndAttackTransition instance = null;
    public static MB08EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB08EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB08Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB08MoveState.Instance, this);
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
