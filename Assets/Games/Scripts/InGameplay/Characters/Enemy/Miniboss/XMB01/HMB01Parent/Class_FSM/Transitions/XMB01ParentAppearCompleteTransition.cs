
using Class_FSM;

public class XMB01ParentAppearCompleteTransition : XMB01ParentTransition {

    #region Singleton
    public XMB01ParentAppearCompleteTransition() {

    }
    private static XMB01ParentAppearCompleteTransition instance = null;
    public static XMB01ParentAppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentAppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XMB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MinibossMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(XMB01ParentIdleState.Instance, this);
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
