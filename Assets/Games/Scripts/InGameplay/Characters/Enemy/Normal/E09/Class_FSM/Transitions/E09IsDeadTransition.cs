

using Class_FSM;

public class E09IsDeadTransition : E09Transition {
    #region Singleton
    public E09IsDeadTransition() {

    }
    private static E09IsDeadTransition instance = null;
    public static E09IsDeadTransition Instance {
        get {
            if(instance == null) {
                instance = new E09IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E09Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(E09DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E09Base> controller) {
    }
}
