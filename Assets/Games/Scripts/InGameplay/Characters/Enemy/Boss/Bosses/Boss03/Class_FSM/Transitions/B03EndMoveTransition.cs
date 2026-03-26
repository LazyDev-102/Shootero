

using Class_FSM;

public class B03EndMoveTransition : B03Transition {
    #region Singleton
    public B03EndMoveTransition() {

    }
    private static B03EndMoveTransition instance = null;
    public static B03EndMoveTransition Instance {
        get {
            if(instance == null) {
                instance = new B03EndMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.B03Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B03IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B03Base> controller) {
    }
}
