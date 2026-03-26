

using Class_FSM;

public class B03CanAppearTransition : B03Transition {
    #region Singleton
    public B03CanAppearTransition() {

    }
    private static B03CanAppearTransition instance = null;
    public static B03CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B03CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.B03Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B03AppearState.Instance, this);
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
