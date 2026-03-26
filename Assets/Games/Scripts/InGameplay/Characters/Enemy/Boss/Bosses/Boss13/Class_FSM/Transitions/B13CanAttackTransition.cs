

using Class_FSM;
using System;
using System.Collections.Generic;

public class B13CanAttackTransition : B13Transition {
    #region Singleton
    public B13CanAttackTransition() {

    }
    private static B13CanAttackTransition instance = null;
    public static B13CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B13CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B13Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B13Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B13AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B13Base> controller) {
    }
}
