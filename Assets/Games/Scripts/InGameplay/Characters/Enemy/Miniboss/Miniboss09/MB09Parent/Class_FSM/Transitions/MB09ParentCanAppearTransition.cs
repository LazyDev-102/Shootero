using Class_FSM;

public class MB09ParentCanAppearTransition : MB09ParentTransition {

    #region Singleton
    public MB09ParentCanAppearTransition() {

    }
    private static MB09ParentCanAppearTransition instance = null;
    public static MB09ParentCanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentCanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB09ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MB09ParentMove.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB09ParentAppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB09ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB09ParentBase> controller) {
    }
}
