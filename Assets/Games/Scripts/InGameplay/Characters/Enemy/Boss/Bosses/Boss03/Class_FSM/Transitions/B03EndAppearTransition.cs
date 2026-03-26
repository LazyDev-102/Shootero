

using Class_FSM;

public class B03EndAppearTransition : B03Transition {
    #region Singleton
    public B03EndAppearTransition() {

    }
    private static B03EndAppearTransition instance = null;
    public static B03EndAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B03EndAppearTransition();
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
