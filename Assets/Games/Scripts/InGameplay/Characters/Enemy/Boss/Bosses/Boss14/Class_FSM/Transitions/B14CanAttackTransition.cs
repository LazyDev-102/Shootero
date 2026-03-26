

using Class_FSM;
using System;
using System.Collections.Generic;

public class B14CanAttackTransition : B14Transition {
    #region Singleton
    public B14CanAttackTransition() {

    }
    private static B14CanAttackTransition instance = null;
    public static B14CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B14CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B14Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B14Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B14AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14Base> controller) {
    }
}
