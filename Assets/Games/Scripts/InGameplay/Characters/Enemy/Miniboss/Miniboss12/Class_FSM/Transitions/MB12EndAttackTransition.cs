using Class_FSM;
using UnityEngine;

public class MB12EndAttackTransition : MB12Transition {

    #region Singleton
    public MB12EndAttackTransition() {

    }
    private static MB12EndAttackTransition instance = null;
    public static MB12EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB12EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB12Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB12MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB12Base> controller) {
    }
}
