using Class_FSM;
using UnityEngine;

public class MB07CanAttackTransition : MB07Transition {

    #region Singleton
    public MB07CanAttackTransition() {

    }
    private static MB07CanAttackTransition instance = null;
    public static MB07CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB07CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB07Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB07Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB07AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB07Base> controller) {
    }
}
