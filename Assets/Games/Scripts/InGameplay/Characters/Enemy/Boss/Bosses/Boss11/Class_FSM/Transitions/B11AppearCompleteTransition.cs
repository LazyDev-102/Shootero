

using Class_FSM;

public class B11AppearCompleteTransition : B11Transition {
    #region Singleton
    public B11AppearCompleteTransition() {

    }
    private static B11AppearCompleteTransition instance = null;
    public static B11AppearCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B11AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B11Base> controller) {
        bool isTransition = controller.ObjectBase.B11Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B11IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B11Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<B11Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<B11Base> controller) {

    }
}
