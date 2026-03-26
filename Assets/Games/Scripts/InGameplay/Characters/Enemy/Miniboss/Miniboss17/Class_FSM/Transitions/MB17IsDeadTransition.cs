using Class_FSM;

public class MB17IsDeadTransition : MB17Transition {

    #region Singleton
    public MB17IsDeadTransition() {

    }
    private static MB17IsDeadTransition instance = null;
    public static MB17IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB17IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB17Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB17DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB17Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB17Base> controller) {
    }
}
