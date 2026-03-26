using Class_FSM;

public class XMB02IsDeadTransition : XMB02Transition {

    #region Singleton
    public XMB02IsDeadTransition() {

    }
    private static XMB02IsDeadTransition instance = null;
    public static XMB02IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB02IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB02Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(XMB02DeadState.Instance, this);
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
