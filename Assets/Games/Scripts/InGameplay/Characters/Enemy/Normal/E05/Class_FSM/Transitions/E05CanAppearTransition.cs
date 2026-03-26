

using Class_FSM;

public class E05CanAppearTransition : E05Transition {
    #region Singleton
    public E05CanAppearTransition() {

    }
    private static E05CanAppearTransition instance = null;
    public static E05CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E05CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E05Base> controller) {
        bool isTransition = controller.ObjectBase.E05Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E05MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E05Base> controller) {
    }
}
