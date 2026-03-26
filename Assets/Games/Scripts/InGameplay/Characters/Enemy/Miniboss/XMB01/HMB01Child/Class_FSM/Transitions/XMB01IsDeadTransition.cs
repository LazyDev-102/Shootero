using Class_FSM;

public class XMB01IsDeadTransition : XMB01Transition {

    #region Singleton
    public XMB01IsDeadTransition() {

    }
    private static XMB01IsDeadTransition instance = null;
    public static XMB01IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(XMB01DeadState.Instance, this);
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
