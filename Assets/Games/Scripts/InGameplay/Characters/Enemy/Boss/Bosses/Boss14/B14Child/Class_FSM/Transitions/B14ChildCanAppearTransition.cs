using Class_FSM;

public class B14ChildCanAppearTransition : B14ChildTransition {

    #region Singleton
    public B14ChildCanAppearTransition() {

    }
    private static B14ChildCanAppearTransition instance = null;
    public static B14ChildCanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B14ChildCanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B14ChildBase> controller) {
        bool isTransition = controller.ObjectBase.B14ChildMove.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(B14ChildAppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14ChildBase> controller) {
    }
}
