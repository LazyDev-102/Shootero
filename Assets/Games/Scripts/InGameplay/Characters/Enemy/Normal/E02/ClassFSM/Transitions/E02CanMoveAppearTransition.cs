

using Class_FSM;

public class E02CanMoveAppearTransition : E02Transition {
    #region Singleton
    public E02CanMoveAppearTransition() {

    }
    private static E02CanMoveAppearTransition instance = null;
    public static E02CanMoveAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E02CanMoveAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<E02Base> controller) {
        bool isTransition = controller.ObjectBase.E02Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E02MoveAppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E02Base> controller) {
    }
}
