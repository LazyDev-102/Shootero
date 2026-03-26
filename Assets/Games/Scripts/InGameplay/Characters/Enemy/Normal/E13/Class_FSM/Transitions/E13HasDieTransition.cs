

using Class_FSM;

public class E13HasDieTransition : E13Transition {
    #region Singleton
    public E13HasDieTransition() {

    }
    private static E13HasDieTransition instance = null;
    public static E13HasDieTransition Instance {
        get {
            if (instance == null) {
                instance = new E13HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E13Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(E13DeadState.Instance, this);
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
