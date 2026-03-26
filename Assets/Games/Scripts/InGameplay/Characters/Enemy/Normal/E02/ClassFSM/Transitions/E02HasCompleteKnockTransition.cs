

using Class_FSM;

public class E02HasCompleteKnockTransition : E02Transition {
    #region Singleton
    public E02HasCompleteKnockTransition() {

    }
    private static E02HasCompleteKnockTransition instance = null;
    public static E02HasCompleteKnockTransition Instance {
        get {
            if (instance == null) {
                instance = new E02HasCompleteKnockTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E02Base> controller) {
        bool isTransition = controller.ObjectBase.E02Move.IsKnockbackCompleted;
        if (isTransition) {
            controller.TransitionToState(E02AttackState.Instance, this);
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
