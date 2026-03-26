

using Class_FSM;

public class E12CanAppearTransition : E12Transition {
    #region Singleton
    public E12CanAppearTransition() {

    }
    private static E12CanAppearTransition instance = null;
    public static E12CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E12CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E12Base> controller) {
        bool isTransition = controller.ObjectBase.E12Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E12MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E12Base> controller) {
    }
}
