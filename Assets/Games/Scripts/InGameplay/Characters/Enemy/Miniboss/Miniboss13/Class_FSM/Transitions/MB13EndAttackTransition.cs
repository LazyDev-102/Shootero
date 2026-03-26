using Class_FSM;
using UnityEngine;

public class MB13EndAttackTransition : MB13Transition {

    #region Singleton
    public MB13EndAttackTransition() {

    }
    private static MB13EndAttackTransition instance = null;
    public static MB13EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB13EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB13Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB13MoveState.Instance, this);
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
