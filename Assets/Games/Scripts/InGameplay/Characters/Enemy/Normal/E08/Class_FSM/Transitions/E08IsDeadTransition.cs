

using Class_FSM;

public class E08IsDeadTransition : E08Transition {
    #region Singleton
    public E08IsDeadTransition() {

    }
    private static E08IsDeadTransition instance = null;
    public static E08IsDeadTransition Instance {
        get {
            if(instance == null) {
                instance = new E08IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E08Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(E08DeadState.Instance, this);
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
