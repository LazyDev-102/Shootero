using Class_FSM;
using UnityEngine;

public class MB07EndAttackTransition : MB07Transition {

    #region Singleton
    public MB07EndAttackTransition() {

    }
    private static MB07EndAttackTransition instance = null;
    public static MB07EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB07EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB07Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB07MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB07Base> controller) {
    }
}
