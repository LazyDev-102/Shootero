

using Class_FSM;

public class E02CanDeadTransition : E02Transition {
    #region Singleton
    public E02CanDeadTransition() {

    }
    private static E02CanDeadTransition instance = null;
    public static E02CanDeadTransition Instance {
        get {
            if(instance == null) {
                instance = new E02CanDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E02Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(E02DeadState.Instance, this);
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
