using Class_FSM;
using UnityEngine;

public class MB15ParentCanAttackTransition : MB15ParentTransition {

    #region Singleton
    public MB15ParentCanAttackTransition() {

    }
    private static MB15ParentCanAttackTransition instance = null;
    public static MB15ParentCanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentCanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB15ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB15ParentAttack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB15ParentAttackState.Instance, this);
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
