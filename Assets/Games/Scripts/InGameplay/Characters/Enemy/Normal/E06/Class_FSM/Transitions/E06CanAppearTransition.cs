

using Class_FSM;

public class E06CanAppearTransition : E06Transition {
    #region Singleton
    public E06CanAppearTransition() {

    }
    private static E06CanAppearTransition instance = null;
    public static E06CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E06CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E06Base> controller) {
        bool isTransition = controller.ObjectBase.E06Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E06MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E06Base> controller) {
    }
}
