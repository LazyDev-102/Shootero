

using Class_FSM;

public class B01CanAppearTransition : B01Transition {
    #region Singleton
    public B01CanAppearTransition() {

    }
    private static B01CanAppearTransition instance = null;
    public static B01CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B01CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B01Base> controller) {
        bool isTransition = controller.ObjectBase.B01Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B01AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B01Base> controller) {
    }
}
