using Class_FSM;

public class MB15ChildCanAppearTransition : MB15ChildTransition {

    #region Singleton
    public MB15ChildCanAppearTransition() {

    }
    private static MB15ChildCanAppearTransition instance = null;
    public static MB15ChildCanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildCanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB15ChildBase> controller) {
        bool isTransition = controller.ObjectBase.MB15ChildMove.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB15ChildAppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB15ChildBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB15ChildBase> controller) {
    }
}
