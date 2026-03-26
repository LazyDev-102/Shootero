using Class_FSM;
using UnityEngine;

public class MB01EndAttackTransition : MB01Transition {

    #region Singleton
    public MB01EndAttackTransition() {

    }
    private static MB01EndAttackTransition instance = null;
    public static MB01EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB01Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB01MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01Base> controller) {
    }
}
