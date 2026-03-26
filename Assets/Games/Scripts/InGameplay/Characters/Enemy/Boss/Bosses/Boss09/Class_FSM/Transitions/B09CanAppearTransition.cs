

using Class_FSM;

public class B09CanAppearTransition : B09Transition {
    #region Singleton
    public B09CanAppearTransition() {

    }
    private static B09CanAppearTransition instance = null;
    public static B09CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B09CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09Base> controller) {
        bool isTransition = controller.ObjectBase.B09Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B09AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09Base> controller) {
    }
}
