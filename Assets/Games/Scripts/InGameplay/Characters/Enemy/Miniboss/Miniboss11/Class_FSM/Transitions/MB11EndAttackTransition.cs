using Class_FSM;
using UnityEngine;

public class MB11EndAttackTransition : MB11Transition {

    #region Singleton
    public MB11EndAttackTransition() {

    }
    private static MB11EndAttackTransition instance = null;
    public static MB11EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB11EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB11Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB11MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB11Base> controller) {
    }
}
