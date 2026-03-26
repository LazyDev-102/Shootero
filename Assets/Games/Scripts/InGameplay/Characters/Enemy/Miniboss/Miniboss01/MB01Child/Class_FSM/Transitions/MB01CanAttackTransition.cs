using Class_FSM;
using UnityEngine;

public class MB01CanAttackTransition : MB01Transition {

    #region Singleton
    public MB01CanAttackTransition() {

    }
    private static MB01CanAttackTransition instance = null;
    public static MB01CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB01Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB01AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01Base> controller) {
    }
}
