

using Class_FSM;

public class B07CanAppearTransition : B07Transition {
    #region Singleton
    public B07CanAppearTransition() {

    }
    private static B07CanAppearTransition instance = null;
    public static B07CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B07CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B07Base> controller) {
        bool isTransition = controller.ObjectBase.B07Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(B07AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B07Base> controller) {
    }
}
