

using Class_FSM;

public class B12ChildHasCompleteAppearTranstion : B12ChildTransition{
    #region Singleton
    public B12ChildHasCompleteAppearTranstion() {

    }
    private static B12ChildHasCompleteAppearTranstion instance = null;
    public static B12ChildHasCompleteAppearTranstion Instance {
        get {
            if(instance == null) {
                instance = new B12ChildHasCompleteAppearTranstion();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B12ChildBase> controller) {
        bool isTransition = controller.ObjectBase.B12ChildMove.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B12ChildAttackState.Instance, this);
        }

        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B12ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12ChildBase> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B12ChildBase> controller) {
    }
}
