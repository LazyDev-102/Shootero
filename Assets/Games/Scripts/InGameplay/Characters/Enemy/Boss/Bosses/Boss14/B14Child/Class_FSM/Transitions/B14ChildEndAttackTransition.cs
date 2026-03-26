using Class_FSM;
using UnityEngine;

public class B14ChildEndAttackTransition : B14ChildTransition {

    #region Singleton
    public B14ChildEndAttackTransition() {

    }
    private static B14ChildEndAttackTransition instance = null;
    public static B14ChildEndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B14ChildEndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B14ChildBase> controller) {
        bool isTransition = !controller.ObjectBase.BossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B14ChildMoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14ChildBase> controller) {
    }
}
