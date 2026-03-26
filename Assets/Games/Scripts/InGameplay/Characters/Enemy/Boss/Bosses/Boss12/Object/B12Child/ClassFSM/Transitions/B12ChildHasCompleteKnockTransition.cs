

using Class_FSM;

public class B12ChildHasCompleteKnockTransition : B12ChildTransition {
    #region Singleton
    public B12ChildHasCompleteKnockTransition() {

    }
    private static B12ChildHasCompleteKnockTransition instance = null;
    public static B12ChildHasCompleteKnockTransition Instance {
        get {
            if (instance == null) {
                instance = new B12ChildHasCompleteKnockTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12ChildBase> controller) {
        bool isTransition = controller.ObjectBase.B12ChildMove.IsKnockbackCompleted;
        if (isTransition) {
            controller.TransitionToState(B12ChildAttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B12ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12ChildBase> controller) {
    }
}
