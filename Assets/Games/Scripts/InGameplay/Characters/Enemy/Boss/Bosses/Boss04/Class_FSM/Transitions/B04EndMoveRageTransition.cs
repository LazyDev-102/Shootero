

using Class_FSM;

public class B04EndMoveRageTransition : B04Transition {
    #region Singleton
    public B04EndMoveRageTransition() {

    }
    private static B04EndMoveRageTransition instance = null;
    public static B04EndMoveRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B04EndMoveRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B04Base> controller) {
        bool isTransition = controller.ObjectBase.B04Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(B04AttackRageState.Instance, this);
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
