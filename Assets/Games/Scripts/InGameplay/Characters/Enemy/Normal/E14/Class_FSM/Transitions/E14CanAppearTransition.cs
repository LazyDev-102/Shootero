

using Class_FSM;

public class E14CanAppearTransition : E14Transition {
    #region Singleton
    public E14CanAppearTransition() {

    }
    private static E14CanAppearTransition instance = null;
    public static E14CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E14CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E14Base> controller) {
        bool isTransition = controller.ObjectBase.E14Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E14MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E14Base> controller) {
    }
}
