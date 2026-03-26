

using Class_FSM;

public class B11MoveCompleteTransition : B11Transition {
    #region Singleton
    public B11MoveCompleteTransition() {

    }
    private static B11MoveCompleteTransition instance = null;
    public static B11MoveCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B11MoveCompleteTransition();
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
