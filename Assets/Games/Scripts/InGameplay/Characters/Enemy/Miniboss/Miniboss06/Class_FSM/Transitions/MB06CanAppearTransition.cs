using Class_FSM;

public class MB06CanAppearTransition : MB06Transition {

    #region Singleton
    public MB06CanAppearTransition() {

    }
    private static MB06CanAppearTransition instance = null;
    public static MB06CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB06CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB06Base> controller) {
        bool isTransition = controller.ObjectBase.MB06Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB06AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB06Base> controller) {
    }
}
