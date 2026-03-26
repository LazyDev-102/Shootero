using Class_FSM;

public class MB02IsDeadTransition : MB02Transition {

    #region Singleton
    public MB02IsDeadTransition() {

    }
    private static MB02IsDeadTransition instance = null;
    public static MB02IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB02IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB02Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB02DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB02Base> controller) {
    }
}
