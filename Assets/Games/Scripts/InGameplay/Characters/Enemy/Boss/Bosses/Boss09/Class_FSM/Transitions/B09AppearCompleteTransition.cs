

using Class_FSM;

public class B09AppearCompleteTransition : B09Transition {
    #region Singleton
    public B09AppearCompleteTransition() {

    }
    private static B09AppearCompleteTransition instance = null;
    public static B09AppearCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B09AppearCompleteTransition();
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
