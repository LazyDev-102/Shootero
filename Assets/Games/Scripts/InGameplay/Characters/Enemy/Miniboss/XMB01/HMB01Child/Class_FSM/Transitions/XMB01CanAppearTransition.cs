using Class_FSM;

public class XMB01CanAppearTransition : XMB01Transition {

    #region Singleton
    public XMB01CanAppearTransition() {

    }
    private static XMB01CanAppearTransition instance = null;
    public static XMB01CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB01Base> controller) {
        bool isTransition = controller.ObjectBase.XMB01Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(XMB01AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<XMB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<XMB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<XMB01Base> controller) {
    }
}
