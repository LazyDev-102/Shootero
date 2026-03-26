

using Class_FSM;

public class B14CanAppearTransition : B14Transition {
    #region Singleton
    public B14CanAppearTransition() {

    }
    private static B14CanAppearTransition instance = null;
    public static B14CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B14CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B14Base> controller) {
        bool isTransition = controller.ObjectBase.B14Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B14AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14Base> controller) {
    }
}
