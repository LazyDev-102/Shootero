using Class_FSM;
using UnityEngine;

public class MB14EndSpecialTransition : MB14Transition {

    #region Singleton
    public MB14EndSpecialTransition() {

    }
    private static MB14EndSpecialTransition instance = null;
    public static MB14EndSpecialTransition Instance {
        get {
            if (instance == null) {
                instance = new MB14EndSpecialTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB14Base> controller) {
        bool isTransition = !controller.ObjectBase.MB14Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(MB14MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB14Base> controller) {
    }
}
