

using Class_FSM;

public class XB01CanAppearTransition : XB01Transition {
    #region Singleton
    public XB01CanAppearTransition() {

    }
    private static XB01CanAppearTransition instance = null;
    public static XB01CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new XB01CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XB01Base> controller) {
        bool isTransition = controller.ObjectBase.XB01Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(XB01AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<XB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<XB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<XB01Base> controller) {
    }
}
