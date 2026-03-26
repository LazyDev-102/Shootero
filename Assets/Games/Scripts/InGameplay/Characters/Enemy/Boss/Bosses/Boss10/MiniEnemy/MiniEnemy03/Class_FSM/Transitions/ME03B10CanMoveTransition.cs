using Class_FSM;

public class ME03B10CanMoveTransition : ME03B10Transition {
    #region Singleton
    public ME03B10CanMoveTransition() {

    }
    private static ME03B10CanMoveTransition instance = null;
    public static ME03B10CanMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new ME03B10CanMoveTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ME03B10Base> controller) {
        bool isTransition = controller.ObjectBase.ME03B10Move.CanMovePoint();
        if (isTransition) {
            controller.TransitionToState(ME03B10MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ME03B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ME03B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ME03B10Base> controller) {
    }
}
