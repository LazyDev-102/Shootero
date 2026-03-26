

using Class_FSM;

public class ME01IsDeadTransition : ME01Transition {
    #region Singleton
    public ME01IsDeadTransition() {

    }
    private static ME01IsDeadTransition instance = null;
    public static ME01IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new ME01IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<ME01Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(ME01DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ME01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ME01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ME01Base> controller) {
    }
}
