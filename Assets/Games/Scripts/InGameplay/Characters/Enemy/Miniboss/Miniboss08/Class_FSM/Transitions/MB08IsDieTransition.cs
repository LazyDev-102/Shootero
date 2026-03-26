using Class_FSM;
using UnityEngine;

public class MB08IsDieTransition : MB08Transition {

    #region Singleton
    public MB08IsDieTransition() {

    }
    private static MB08IsDieTransition instance = null;
    public static MB08IsDieTransition Instance {
        get {
            if (instance == null) {
                instance = new MB08IsDieTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB08Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB08DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB08Base> controller) {
    }
}
