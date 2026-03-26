using Class_FSM;
using UnityEngine;

public class MB10EndAttackTransition : MB10Transition {

    #region Singleton
    public MB10EndAttackTransition() {

    }
    private static MB10EndAttackTransition instance = null;
    public static MB10EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB10EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB10Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB10MoveState.Instance, this);
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
