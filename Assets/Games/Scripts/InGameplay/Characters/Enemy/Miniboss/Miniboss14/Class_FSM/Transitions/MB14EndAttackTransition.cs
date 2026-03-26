using Class_FSM;
using UnityEngine;

public class MB14EndAttackTransition : MB14Transition {

    #region Singleton
    public MB14EndAttackTransition() {

    }
    private static MB14EndAttackTransition instance = null;
    public static MB14EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB14EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB14Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB14MoveState.Instance, this);
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
