

using Class_FSM;

public class ME03B08CanMoveTransition : ME03B08Transition {
    #region Singleton
    public ME03B08CanMoveTransition() {

    }
    private static ME03B08CanMoveTransition instance = null;
    public static ME03B08CanMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new ME03B08CanMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<ME03B08Base> controller) {
        bool isTransition = controller.ObjectBase.CanMove;
        if (isTransition) {
            controller.TransitionToState(ME03B08MoveState.Instance, this);
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
