

using Class_FSM;

public class B02CanAppearTransition : B02Transition {
    #region Singleton
    public B02CanAppearTransition() {

    }
    private static B02CanAppearTransition instance = null;
    public static B02CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B02CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B02Base> controller) {
        bool isTransition = controller.ObjectBase.B02Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B02AppearState.Instance, this);
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
