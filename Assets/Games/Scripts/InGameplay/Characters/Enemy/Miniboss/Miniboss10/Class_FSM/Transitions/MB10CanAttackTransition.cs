using Class_FSM;
using UnityEngine;

public class MB10CanAttackTransition : MB10Transition {

    #region Singleton
    public MB10CanAttackTransition() {

    }
    private static MB10CanAttackTransition instance = null;
    public static MB10CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB10CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB10Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB10Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB10AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB10Base> controller) {
    }
}
