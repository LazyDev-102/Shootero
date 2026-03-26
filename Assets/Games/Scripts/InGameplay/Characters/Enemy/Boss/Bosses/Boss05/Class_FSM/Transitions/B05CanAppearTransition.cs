

using Class_FSM;

public class B05CanAppearTransition : B05Transition {
    #region Singleton
    public B05CanAppearTransition() {

    }
    private static B05CanAppearTransition instance = null;
    public static B05CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B05CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B05Base> controller) {
        bool isTransition = controller.ObjectBase.B05Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B05AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B05Base> controller) {
    }
}
