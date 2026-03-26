using Class_FSM;
using UnityEngine;

public class MB09ParentEndAttackTransition : MB09ParentTransition {

    #region Singleton
    public MB09ParentEndAttackTransition() {

    }
    private static MB09ParentEndAttackTransition instance = null;
    public static MB09ParentEndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentEndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB09ParentBase> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB09ParentMoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09ParentBase> controller) {
    }
}
