using Class_FSM;

public class MB07CanAppearTransition : MB07Transition {

    #region Singleton
    public MB07CanAppearTransition() {

    }
    private static MB07CanAppearTransition instance = null;
    public static MB07CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB07CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB07Base> controller) {
        bool isTransition = controller.ObjectBase.MB07Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB07AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB07Base> controller) {
    }
}
