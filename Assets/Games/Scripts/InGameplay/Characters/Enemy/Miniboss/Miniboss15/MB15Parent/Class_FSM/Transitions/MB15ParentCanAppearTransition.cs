using Class_FSM;

public class MB15ParentCanAppearTransition : MB15ParentTransition {

    #region Singleton
    public MB15ParentCanAppearTransition() {

    }
    private static MB15ParentCanAppearTransition instance = null;
    public static MB15ParentCanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentCanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB15ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MB15ParentMove.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB15ParentAppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ParentBase> controller) {
    }
}
