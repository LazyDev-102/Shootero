

using Class_FSM;

public class E10IsDeadTransition : E10Transition {
    #region Singleton
    public E10IsDeadTransition() {

    }
    private static E10IsDeadTransition instance = null;
    public static E10IsDeadTransition Instance {
        get {
            if(instance == null) {
                instance = new E10IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E10Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(E10DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E10Base> controller) {
    }
}
