

using Class_FSM;

public class B12CanAppearTransition : B12Transition {
    #region Singleton
    public B12CanAppearTransition() {

    }
    private static B12CanAppearTransition instance = null;
    public static B12CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B12CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12Base> controller) {
        bool isTransition = controller.ObjectBase.B12Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B12AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12Base> controller) {
    }
}
