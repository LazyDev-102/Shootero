

using Class_FSM;

public class E08CanAppearTransition : E08Transition {
    #region Singleton
    public E08CanAppearTransition() {

    }
    private static E08CanAppearTransition instance = null;
    public static E08CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E08CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E08Base> controller) {
        bool isTransition = controller.ObjectBase.E08Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E08AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E08Base> controller) {
    }
}
