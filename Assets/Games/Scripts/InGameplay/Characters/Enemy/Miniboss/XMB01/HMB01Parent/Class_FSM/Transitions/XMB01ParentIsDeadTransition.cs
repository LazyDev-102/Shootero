using Class_FSM;

public class XMB01ParentIsDeadTransition : XMB01ParentTransition {

    #region Singleton
    public XMB01ParentIsDeadTransition() {

    }
    private static XMB01ParentIsDeadTransition instance = null;
    public static XMB01ParentIsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentIsDeadTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(XMB01ParentDeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<XMB01ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<XMB01ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<XMB01ParentBase> controller) {
    }
}
