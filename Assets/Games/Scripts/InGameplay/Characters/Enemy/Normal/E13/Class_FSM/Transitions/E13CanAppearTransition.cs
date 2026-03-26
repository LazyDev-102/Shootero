

using Class_FSM;

public class E13CanAppearTransition : E13Transition {
    #region Singleton
    public E13CanAppearTransition() {

    }
    private static E13CanAppearTransition instance = null;
    public static E13CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E13CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E13Base> controller) {
        bool isTransition = controller.ObjectBase.E13Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E13MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E13Base> controller) {
    }
}
