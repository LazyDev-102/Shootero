

using Class_FSM;

public class B04EndMoveTransition : B04Transition {
    #region Singleton
    public B04EndMoveTransition() {

    }
    private static B04EndMoveTransition instance = null;
    public static B04EndMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new B04EndMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B04Base> controller) {
        bool isTransition = controller.ObjectBase.B04Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B04IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B04Base> controller) {
    }
}
