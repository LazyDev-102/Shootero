using Class_FSM;
using UnityEngine;

public class MB05EndAttackTransition : MB05Transition {

    #region Singleton
    public MB05EndAttackTransition() {

    }
    private static MB05EndAttackTransition instance = null;
    public static MB05EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB05EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB05Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB05MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB05Base> controller) {
    }
}
