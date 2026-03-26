

using Class_FSM;

public class B01MoveCompleteTransition : B01Transition {
    #region Singleton
    public B01MoveCompleteTransition() {

    }
    private static B01MoveCompleteTransition instance = null;
    public static B01MoveCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new B01MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B01Base> controller) {
        bool isTransition = controller.ObjectBase.B01Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B01IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B01Base> controller) {
    }
}
