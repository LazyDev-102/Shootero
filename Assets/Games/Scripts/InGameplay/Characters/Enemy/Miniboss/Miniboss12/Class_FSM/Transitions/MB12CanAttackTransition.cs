using Class_FSM;
using UnityEngine;

public class MB12CanAttackTransition : MB12Transition {

    #region Singleton
    public MB12CanAttackTransition() {

    }
    private static MB12CanAttackTransition instance = null;
    public static MB12CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB12CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB12Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB12Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB12AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB12Base> controller) {
    }
}
