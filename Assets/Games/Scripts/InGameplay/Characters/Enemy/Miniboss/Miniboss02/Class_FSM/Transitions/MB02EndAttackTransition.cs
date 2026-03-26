using Class_FSM;
using UnityEngine;

public class MB02EndAttackTransition : MB02Transition {

    #region Singleton
    public MB02EndAttackTransition() {

    }
    private static MB02EndAttackTransition instance = null;
    public static MB02EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB02EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB02Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB02MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB02Base> controller) {
    }
}
