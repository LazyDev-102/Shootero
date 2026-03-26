using Class_FSM;

public class XMB02CanAppearTransition : XMB02Transition {

    #region Singleton
    public XMB02CanAppearTransition() {

    }
    private static XMB02CanAppearTransition instance = null;
    public static XMB02CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB02CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB02Base> controller) {
        bool isTransition = controller.ObjectBase.XMB02Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(XMB02AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<XMB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<XMB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<XMB02Base> controller) {
    }
}
