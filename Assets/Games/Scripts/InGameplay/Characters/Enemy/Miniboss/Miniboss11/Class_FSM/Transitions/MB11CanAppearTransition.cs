using Class_FSM;

public class MB11CanAppearTransition : MB11Transition {

    #region Singleton
    public MB11CanAppearTransition() {

    }
    private static MB11CanAppearTransition instance = null;
    public static MB11CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new MB11CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<MB11Base> controller) {
        bool isTransition = controller.ObjectBase.MB11Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(MB11AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MB11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MB11Base> controller) {
    }
}
