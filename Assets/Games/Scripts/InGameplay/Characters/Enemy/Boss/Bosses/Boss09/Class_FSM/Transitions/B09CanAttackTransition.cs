

using Class_FSM;
using System;
using System.Collections.Generic;

public class B09CanAttackTransition : B09Transition {
    #region Singleton
    public B09CanAttackTransition() {

    }
    private static B09CanAttackTransition instance = null;
    public static B09CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B09CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    
    public override bool CheckTransition(StateController<B09Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B09Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(B09AttackState.Instance, this);
            //controller.TransitionToState(controller.ObjectBase.B09Attack.ChooseState(), this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09Base> controller) {
    }
}
