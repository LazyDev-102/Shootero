using Class_FSM;
using UnityEngine;

public class MB06EndAttackTransition : MB06Transition {

    #region Singleton
    public MB06EndAttackTransition() {

    }
    private static MB06EndAttackTransition instance = null;
    public static MB06EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new MB06EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB06Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB06MoveState.Instance, this);
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
