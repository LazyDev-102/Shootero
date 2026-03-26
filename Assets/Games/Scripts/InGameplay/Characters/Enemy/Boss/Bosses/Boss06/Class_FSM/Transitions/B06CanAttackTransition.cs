

using Class_FSM;
using System;
using System.Collections.Generic;

public class B06CanAttackTransition : B06Transition {
    #region Singleton
    public B06CanAttackTransition() {

    }
    private static B06CanAttackTransition instance = null;
    public static B06CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B06CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    
    public override bool CheckTransition(StateController<B06Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B06Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(B06AttackState.Instance, this);
            //controller.TransitionToState(controller.ObjectBase.B06Attack.ChooseState(), this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B06Base> controller) {
    }
}
