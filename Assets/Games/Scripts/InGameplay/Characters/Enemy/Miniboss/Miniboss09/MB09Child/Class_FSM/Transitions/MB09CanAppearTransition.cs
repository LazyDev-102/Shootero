using Class_FSM;

public class MB09CanAppearTransition : MB09Transition {

    #region Singleton
    public MB09CanAppearTransition() {

    }
    private static MB09CanAppearTransition instance = null;
    public static MB09CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB09Base> controller) {
        bool isTransition = controller.ObjectBase.MB09Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB09AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09Base> controller) {
    }
}
