

using Class_FSM;

public class B04CanAppearTransition : B04Transition {
    #region Singleton
    public B04CanAppearTransition() {

    }
    private static B04CanAppearTransition instance = null;
    public static B04CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new B04CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B04Base> controller) {
        bool isTransition = controller.ObjectBase.B04Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(B04AppearState.Instance, this);
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
