using Class_FSM;
using UnityEngine;

public class MB01ParentEndAttackTransition : MB01ParentTransition {

    #region Singleton
    public MB01ParentEndAttackTransition() {

    }
    private static MB01ParentEndAttackTransition instance = null;
    public static MB01ParentEndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentEndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB01ParentBase> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB01ParentMoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01ParentBase> controller) {
    }
}
