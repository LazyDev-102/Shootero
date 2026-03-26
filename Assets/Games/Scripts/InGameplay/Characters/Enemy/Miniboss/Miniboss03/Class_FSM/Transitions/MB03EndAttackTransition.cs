using Class_FSM;
using UnityEngine;

public class MB03EndAttackTransition : MB03Transition {

    #region Singleton
    public MB03EndAttackTransition() {

    }
    private static MB03EndAttackTransition instance = null;
    public static MB03EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB03EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB03Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB03MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB03Base> controller) {
    }
}
