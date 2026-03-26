

using Class_FSM;
using System;
using System.Collections.Generic;

public class B05CanAttackTransition : B05Transition {
    #region Singleton
    public B05CanAttackTransition() {

    }
    private static B05CanAttackTransition instance = null;
    public static B05CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B05CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    
    public override bool CheckTransition(StateController<B05Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B05Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(B05AttackState.Instance, this);
            //controller.TransitionToState(controller.ObjectBase.B05Attack.ChooseState(), this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B05Base> controller) {
    }
}
