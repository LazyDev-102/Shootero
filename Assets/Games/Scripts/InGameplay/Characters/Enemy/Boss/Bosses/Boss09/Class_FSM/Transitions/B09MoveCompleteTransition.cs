

using Class_FSM;

public class B09MoveCompleteTransition : B09Transition {
    #region Singleton
    public B09MoveCompleteTransition() {

    }
    private static B09MoveCompleteTransition instance = null;
    public static B09MoveCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B09MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09Base> controller) {
        bool isTransition = controller.ObjectBase.B09Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B09IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09Base> controller) {
    }
}
