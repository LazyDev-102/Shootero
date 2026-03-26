

using Class_FSM;

public class ME02B08IsDeadTransition : ME02B08Transition {
    #region Singleton
    public ME02B08IsDeadTransition() {

    }
    private static ME02B08IsDeadTransition instance = null;
    public static ME02B08IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new ME02B08IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ME02B08Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(ME02B08DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ME02B08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ME02B08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ME02B08Base> controller) {
    }
}
