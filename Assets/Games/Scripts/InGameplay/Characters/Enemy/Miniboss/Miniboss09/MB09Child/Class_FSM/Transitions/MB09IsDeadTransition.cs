using Class_FSM;

public class MB09IsDeadTransition : MB09Transition {

    #region Singleton
    public MB09IsDeadTransition() {

    }
    private static MB09IsDeadTransition instance = null;
    public static MB09IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB09Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB09DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09Base> controller) {
    }
}
