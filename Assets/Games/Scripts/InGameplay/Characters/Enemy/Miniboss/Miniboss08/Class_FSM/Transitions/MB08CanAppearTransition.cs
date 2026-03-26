using Class_FSM;
using UnityEngine;

public class MB08CanAppearTransition : MB08Transition {

    #region Singleton
    public MB08CanAppearTransition() {

    }
    private static MB08CanAppearTransition instance = null;
    public static MB08CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB08CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB08Base> controller) {
        bool isTransition = controller.ObjectBase.MB08Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB08AppearState.Instance, this);
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
