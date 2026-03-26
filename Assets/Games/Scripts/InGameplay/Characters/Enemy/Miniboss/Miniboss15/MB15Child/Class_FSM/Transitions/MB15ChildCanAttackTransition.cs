using Class_FSM;
using UnityEngine;

public class MB15ChildCanAttackTransition : MB15ChildTransition {

    #region Singleton
    public MB15ChildCanAttackTransition() {

    }
    private static MB15ChildCanAttackTransition instance = null;
    public static MB15ChildCanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildCanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB15ChildBase> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB15ChildAttack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB15ChildAttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ChildBase> controller) {
    }
}
