using Class_FSM;
using UnityEngine;

public class MB04CanAppearTransition : MB04Transition {

    #region Singleton
    public MB04CanAppearTransition() {

    }
    private static MB04CanAppearTransition instance = null;
    public static MB04CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB04CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB04Base> controller) {
        bool isTransition = controller.ObjectBase.MB04Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB04AppearState.Instance, this);
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
