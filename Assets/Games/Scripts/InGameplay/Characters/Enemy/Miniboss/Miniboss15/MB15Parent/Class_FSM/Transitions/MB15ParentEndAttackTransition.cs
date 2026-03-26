using Class_FSM;
using UnityEngine;

public class MB15ParentEndAttackTransition : MB15ParentTransition {

    #region Singleton
    public MB15ParentEndAttackTransition() {

    }
    private static MB15ParentEndAttackTransition instance = null;
    public static MB15ParentEndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentEndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB15ParentBase> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB15ParentMoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ParentBase> controller) {
    }
}
