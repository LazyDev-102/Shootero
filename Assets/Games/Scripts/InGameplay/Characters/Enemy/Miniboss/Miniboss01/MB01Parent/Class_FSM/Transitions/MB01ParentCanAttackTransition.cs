using Class_FSM;
using UnityEngine;

public class MB01ParentCanAttackTransition : MB01ParentTransition {

    #region Singleton
    public MB01ParentCanAttackTransition() {

    }
    private static MB01ParentCanAttackTransition instance = null;
    public static MB01ParentCanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentCanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB01ParentAttack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB01ParentAttackState.Instance, this);
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
