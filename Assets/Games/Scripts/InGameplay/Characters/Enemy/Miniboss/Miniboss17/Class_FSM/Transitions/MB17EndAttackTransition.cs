using Class_FSM;
using UnityEngine;

public class MB17EndAttackTransition : MB17Transition {

    #region Singleton
    public MB17EndAttackTransition() {

    }
    private static MB17EndAttackTransition instance = null;
    public static MB17EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB17EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB17Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB17MoveState.Instance, this);
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
