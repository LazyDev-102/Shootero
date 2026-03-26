

using Class_FSM;

public class E03CanMoveAppearTransition : E03Transition {
    #region Singleton
    public E03CanMoveAppearTransition() {

    }
    private static E03CanMoveAppearTransition instance = null;
    public static E03CanMoveAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E03CanMoveAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E03Base> controller) {
        bool isTransition = controller.ObjectBase.E03Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E03MoveAppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E03Base> controller) {
    }
}
