using Class_FSM;
using UnityEngine;

public class MB09ParentCanAttackTransition : MB09ParentTransition {

    #region Singleton
    public MB09ParentCanAttackTransition() {

    }
    private static MB09ParentCanAttackTransition instance = null;
    public static MB09ParentCanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentCanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB09ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB09ParentAttack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB09ParentAttackState.Instance, this);
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
