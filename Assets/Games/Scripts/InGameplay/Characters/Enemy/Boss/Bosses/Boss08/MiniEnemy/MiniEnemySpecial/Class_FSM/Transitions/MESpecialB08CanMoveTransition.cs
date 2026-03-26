

using Class_FSM;

public class MESpecialB08CanMoveTransition : MESpecialB08Transition {
    #region Singleton
    public MESpecialB08CanMoveTransition() {

    }
    private static MESpecialB08CanMoveTransition instance = null;
    public static MESpecialB08CanMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new MESpecialB08CanMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MESpecialB08Base> controller) {
        bool isTransition = controller.ObjectBase.IsMoveToTarget && controller.ObjectBase.CanMove;
        if (isTransition) {
            controller.TransitionToState(MESpecialB08MoveState.Instance, this);
        }
        return isTransition;

    }

    public override void DoAfterTransitionActions(StateController<MESpecialB08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MESpecialB08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MESpecialB08Base> controller) {
    }
}
