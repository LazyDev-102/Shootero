

using Class_FSM;

public class ME01CanMoveTransition : ME01Transition {
    #region Singleton
    public ME01CanMoveTransition() {

    }
    private static ME01CanMoveTransition instance = null;
    public static ME01CanMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new ME01CanMoveTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ME01Base> controller) {
        bool isTransition = controller.ObjectBase.CanMove;
        if (isTransition) {
            controller.TransitionToState(ME01MoveState.Instance, this);
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
