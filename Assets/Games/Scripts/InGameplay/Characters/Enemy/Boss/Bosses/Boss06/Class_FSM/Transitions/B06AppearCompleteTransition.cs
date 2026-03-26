

using Class_FSM;

public class B06AppearCompleteTransition : B06Transition {
    #region Singleton
    public B06AppearCompleteTransition() {

    }
    private static B06AppearCompleteTransition instance = null;
    public static B06AppearCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B06AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B06Base> controller) {
        bool isTransition = controller.ObjectBase.B06Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B06IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B06Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<B06Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<B06Base> controller) {

    }
}
