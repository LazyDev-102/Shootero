using Class_FSM;
using UnityEngine;

public class B14ChildCanAttackTransition : B14ChildTransition {

    #region Singleton
    public B14ChildCanAttackTransition() {

    }
    private static B14ChildCanAttackTransition instance = null;
    public static B14ChildCanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B14ChildCanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B14ChildBase> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B14ChildAttack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B14ChildAttackState.Instance, this);
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
