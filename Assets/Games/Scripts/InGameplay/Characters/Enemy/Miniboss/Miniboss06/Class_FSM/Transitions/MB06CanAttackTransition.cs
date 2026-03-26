using Class_FSM;
using UnityEngine;

public class MB06CanAttackTransition : MB06Transition {

    #region Singleton
    public MB06CanAttackTransition() {

    }
    private static MB06CanAttackTransition instance = null;
    public static MB06CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB06CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB06Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB06Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB06AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB06Base> controller) {
    }
}
