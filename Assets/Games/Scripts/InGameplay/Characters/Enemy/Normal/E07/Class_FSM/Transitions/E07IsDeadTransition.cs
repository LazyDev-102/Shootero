

using Class_FSM;

public class E07IsDeadTransition : E07Transition {
    #region Singleton
    public E07IsDeadTransition() {

    }
    private static E07IsDeadTransition instance = null;
    public static E07IsDeadTransition Instance {
        get {
            if(instance == null) {
                instance = new E07IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E07Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(E07DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E07Base> controller) {
    }
}
