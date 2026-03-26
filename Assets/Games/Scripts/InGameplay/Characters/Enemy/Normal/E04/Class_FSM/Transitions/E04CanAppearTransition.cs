

using Class_FSM;

public class E04CanAppearTransition : E04Transition {
    #region Singleton
    public E04CanAppearTransition() {

    }
    private static E04CanAppearTransition instance = null;
    public static E04CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E04CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E04Base> controller) {
        bool isTransition = controller.ObjectBase.E04Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E04MoveAppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E04Base> controller) {
    }
}
