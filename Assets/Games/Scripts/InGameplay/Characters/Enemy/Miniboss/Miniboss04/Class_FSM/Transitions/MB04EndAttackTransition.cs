using Class_FSM;
using UnityEngine;

public class MB04EndAttackTransition : MB04Transition {

    #region Singleton
    public MB04EndAttackTransition() {

    }
    private static MB04EndAttackTransition instance = null;
    public static MB04EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB04EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB04Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB04MoveState.Instance, this);
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
