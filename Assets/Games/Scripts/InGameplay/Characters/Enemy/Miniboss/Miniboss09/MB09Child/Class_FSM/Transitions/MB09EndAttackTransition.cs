using Class_FSM;
using UnityEngine;

public class MB09EndAttackTransition : MB09Transition {

    #region Singleton
    public MB09EndAttackTransition() {

    }
    private static MB09EndAttackTransition instance = null;
    public static MB09EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB09Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB09MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09Base> controller) {
    }
}
