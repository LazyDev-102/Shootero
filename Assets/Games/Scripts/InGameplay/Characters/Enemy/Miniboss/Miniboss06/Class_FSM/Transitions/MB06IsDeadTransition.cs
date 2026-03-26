using Class_FSM;

public class MB06IsDeadTransition : MB06Transition {

    #region Singleton
    public MB06IsDeadTransition() {

    }
    private static MB06IsDeadTransition instance = null;
    public static MB06IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MB06IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB06Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MB06DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB06Base> controller) {
    }
}
