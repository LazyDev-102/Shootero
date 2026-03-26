using Class_FSM;
using UnityEngine;

public class MB16EndAttackTransition : MB16Transition {

    #region Singleton
    public MB16EndAttackTransition() {

    }
    private static MB16EndAttackTransition instance = null;
    public static MB16EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB16EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB16Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB16MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB16Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB16Base> controller) {
    }
}
