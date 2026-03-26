using Class_FSM;
using UnityEngine;

public class MB05CanAttackTransition : MB05Transition {

    #region Singleton
    public MB05CanAttackTransition() {

    }
    private static MB05CanAttackTransition instance = null;
    public static MB05CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB05CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB05Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB05Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB05AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB05Base> controller) {
    }
}
