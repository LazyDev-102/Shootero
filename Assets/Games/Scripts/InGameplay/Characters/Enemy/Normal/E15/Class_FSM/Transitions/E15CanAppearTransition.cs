

using Class_FSM;

public class E15CanAppearTransition : E15Transition {
    #region Singleton
    public E15CanAppearTransition() {

    }
    private static E15CanAppearTransition instance = null;
    public static E15CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E15CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E15Base> controller) {
        bool isTransition = controller.ObjectBase.E15Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E15MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E15Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E15Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E15Base> controller) {
    }
}
