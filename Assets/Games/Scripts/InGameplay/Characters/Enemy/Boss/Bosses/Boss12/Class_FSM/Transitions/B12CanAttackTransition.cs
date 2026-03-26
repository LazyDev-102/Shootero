

using Class_FSM;
using System;
using System.Collections.Generic;

public class B12CanAttackTransition : B12Transition {
    #region Singleton
    public B12CanAttackTransition() {

    }
    private static B12CanAttackTransition instance = null;
    public static B12CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B12CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B12Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B12Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B12AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12Base> controller) {
    }
}
