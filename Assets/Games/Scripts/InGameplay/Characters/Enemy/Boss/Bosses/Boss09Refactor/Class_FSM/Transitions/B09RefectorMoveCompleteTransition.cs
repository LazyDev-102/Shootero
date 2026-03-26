

using Class_FSM;

public class B09RefectorMoveCompleteTransition : B09RefectorTransition {
    #region Singleton
    public B09RefectorMoveCompleteTransition() {

    }
    private static B09RefectorMoveCompleteTransition instance = null;
    public static B09RefectorMoveCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B09RefectorMoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09RefectorBase> controller) {
        bool isTransition = controller.ObjectBase.B09RefectorMove.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B09RefectorIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09RefectorBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09RefectorBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09RefectorBase> controller) {
    }
}
