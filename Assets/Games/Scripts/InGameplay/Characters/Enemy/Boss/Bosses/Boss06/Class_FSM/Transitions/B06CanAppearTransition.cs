

using Class_FSM;

public class B06CanAppearTransition : B06Transition {
    #region Singleton
    public B06CanAppearTransition() {

    }
    private static B06CanAppearTransition instance = null;
    public static B06CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B06CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B06Base> controller) {
        bool isTransition = controller.ObjectBase.B06Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B06AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B06Base> controller) {
    }
}
