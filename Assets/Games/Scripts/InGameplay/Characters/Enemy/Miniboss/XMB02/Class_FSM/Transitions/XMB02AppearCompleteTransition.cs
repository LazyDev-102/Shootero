using Class_FSM;

public class XMB02AppearCompleteTransition : XMB02Transition {

    #region Singleton
    public XMB02AppearCompleteTransition() {

    }
    private static XMB02AppearCompleteTransition instance = null;
    public static XMB02AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB02AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XMB02Base> controller) {
        bool isTransition = controller.ObjectBase.XMB02Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(XMB02IdleState.Instance, this);
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
