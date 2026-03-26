
using Class_FSM;

public class XMB01AppearCompleteTransition : XMB01Transition {

    #region Singleton
    public XMB01AppearCompleteTransition() {

    }
    private static XMB01AppearCompleteTransition instance = null;
    public static XMB01AppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01AppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XMB01Base> controller) {
        bool isTransition = controller.ObjectBase.XMB01Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(XMB01IdleState.Instance, this);
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
