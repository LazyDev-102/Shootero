

using Class_FSM;

public class E16CanAppearTransition : E16Transition {
    #region Singleton
    public E16CanAppearTransition() {

    }
    private static E16CanAppearTransition instance = null;
    public static E16CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E16CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E16Base> controller) {
        bool isTransition = controller.ObjectBase.E16Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E16MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E16Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E16Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E16Base> controller) {
    }
}
