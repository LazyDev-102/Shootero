

using Class_FSM;

public class B04EndAppearTransition : B04Transition {
    #region Singleton
    public B04EndAppearTransition() {

    }
    private static B04EndAppearTransition instance = null;
    public static B04EndAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B04EndAppearTransition();
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
