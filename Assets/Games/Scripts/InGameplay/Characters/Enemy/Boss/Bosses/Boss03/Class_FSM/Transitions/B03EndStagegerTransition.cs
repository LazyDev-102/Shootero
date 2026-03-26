

using Class_FSM;

public class B03EndStagegerTransition : B03Transition {
    #region Singleton
    public B03EndStagegerTransition() {

    }
    private static B03EndStagegerTransition instance = null;
    public static B03EndStagegerTransition Instance {
        get {
            if(instance == null) {
                instance = new B03EndStagegerTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndStagger();
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
