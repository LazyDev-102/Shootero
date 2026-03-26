

using Class_FSM;

public class B02EndMoveTransition : B02Transition {
    #region Singleton
    public B02EndMoveTransition() {

    }
    private static B02EndMoveTransition instance = null;
    public static B02EndMoveTransition Instance {
        get {
            if(instance == null) {
                instance = new B02EndMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B02Base> controller) {
        bool isTransition = controller.ObjectBase.B02Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(B02IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B02Base> controller) {
    }
}
