using Class_FSM;
using UnityEngine;

public class MB03CanAttackTransition : MB03Transition {

    #region Singleton
    public MB03CanAttackTransition() {

    }
    private static MB03CanAttackTransition instance = null;
    public static MB03CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB03CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB03Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB03Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB03AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB03Base> controller) {
    }
}
