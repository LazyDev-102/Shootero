using Class_FSM;
using UnityEngine;

public class ME03B10IsDieTransition : ME03B10Transition {
    #region Singleton
    public ME03B10IsDieTransition() {

    }
    private static ME03B10IsDieTransition instance = null;
    public static ME03B10IsDieTransition Instance {
        get {
            if (instance == null) {
                instance = new ME03B10IsDieTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ME03B10Base> controller) {
        bool isTransiton = controller.ObjectBase.IsDie();
        if (isTransiton) {
            controller.TransitionToState(ME03B10DeadState.Instance, this);
        }
        return isTransiton;
    }

    public override void DoAfterTransitionActions(StateController<ME03B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ME03B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ME03B10Base> controller) {
    }
}
