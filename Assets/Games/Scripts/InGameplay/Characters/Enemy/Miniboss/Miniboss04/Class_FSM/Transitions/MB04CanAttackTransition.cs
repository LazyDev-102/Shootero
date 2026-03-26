using Class_FSM;
using UnityEngine;

public class MB04CanAttackTransition : MB04Transition {
    #region Singleton
    public MB04CanAttackTransition() {

    }
    private static MB04CanAttackTransition instance = null;
    public static MB04CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB04CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MB04Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB04Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB04AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB04Base> controller) {
    }
}
