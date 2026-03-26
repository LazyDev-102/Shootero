

using Class_FSM;
using System;
using System.Collections.Generic;

public class B11CanAttackTransition : B11Transition {
    #region Singleton
    public B11CanAttackTransition() {

    }
    private static B11CanAttackTransition instance = null;
    public static B11CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B11CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B11Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B11Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B11AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B11Base> controller) {
    }
}
