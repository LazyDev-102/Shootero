using Class_FSM;

public class MB01ParentCanAppearTransition : MB01ParentTransition {

    #region Singleton
    public MB01ParentCanAppearTransition() {

    }
    private static MB01ParentCanAppearTransition instance = null;
    public static MB01ParentCanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentCanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MB01ParentMove.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB01ParentAppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB01ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB01ParentBase> controller) {
    }
}
