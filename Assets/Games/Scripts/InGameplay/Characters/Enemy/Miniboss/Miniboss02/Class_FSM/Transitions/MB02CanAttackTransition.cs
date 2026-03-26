using Class_FSM;
using UnityEngine;

public class MB02CanAttackTransition : MB02Transition {

    #region Singleton
    public MB02CanAttackTransition() {

    }
    private static MB02CanAttackTransition instance = null;
    public static MB02CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB02CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB02Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB02Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB02AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB02Base> controller) {
    }
}
