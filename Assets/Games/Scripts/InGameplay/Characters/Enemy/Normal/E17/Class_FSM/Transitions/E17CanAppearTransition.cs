

using Class_FSM;

public class E17CanAppearTransition : E17Transition {
    #region Singleton
    public E17CanAppearTransition() {

    }
    private static E17CanAppearTransition instance = null;
    public static E17CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new E17CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E17Base> controller) {
        bool isTransition = controller.ObjectBase.E17Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(E17MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E17Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E17Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E17Base> controller) {
    }
}
