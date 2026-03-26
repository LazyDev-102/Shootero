

using Class_FSM;

public class ME03B08IsDeadTransition : ME03B08Transition {
    #region Singleton
    public ME03B08IsDeadTransition() {

    }
    private static ME03B08IsDeadTransition instance = null;
    public static ME03B08IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new ME03B08IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<ME03B08Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(ME03B08DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ME03B08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ME03B08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ME03B08Base> controller) {
    }
}
