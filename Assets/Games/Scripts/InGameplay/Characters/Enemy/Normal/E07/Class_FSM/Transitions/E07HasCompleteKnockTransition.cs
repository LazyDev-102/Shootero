

using Class_FSM;

public class E07HasCompleteKnockTransition : E07Transition {
    #region Singleton
    public E07HasCompleteKnockTransition() {

    }
    private static E07HasCompleteKnockTransition instance = null;
    public static E07HasCompleteKnockTransition Instance {
        get {
            if (instance == null) {
                instance = new E07HasCompleteKnockTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E07Base> controller) {
        bool isTransition = controller.ObjectBase.E07Move.IsKnockbackCompleted;
        if (isTransition) {
            controller.TransitionToState(E07AimState.Instance, this);
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
