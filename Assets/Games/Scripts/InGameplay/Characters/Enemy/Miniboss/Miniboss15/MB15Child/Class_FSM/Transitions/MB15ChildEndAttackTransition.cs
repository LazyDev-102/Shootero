using Class_FSM;
using UnityEngine;

public class MB15ChildEndAttackTransition : MB15ChildTransition {

    #region Singleton
    public MB15ChildEndAttackTransition() {

    }
    private static MB15ChildEndAttackTransition instance = null;
    public static MB15ChildEndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildEndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB15ChildBase> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB15ChildMoveState.Instance, this);
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
