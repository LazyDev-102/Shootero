using Class_FSM;
using UnityEngine;

public class MB09CanAttackTransition : MB09Transition {

    #region Singleton
    public MB09CanAttackTransition() {

    }
    private static MB09CanAttackTransition instance = null;
    public static MB09CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB09Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.MB09Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(MB09AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09Base> controller) {
    }
}
