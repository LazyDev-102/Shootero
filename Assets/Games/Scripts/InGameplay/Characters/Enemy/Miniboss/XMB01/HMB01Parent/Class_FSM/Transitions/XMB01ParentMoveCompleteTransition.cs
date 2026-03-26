using Class_FSM;
using UnityEngine;

public class XMB01ParentMoveCompleteTransition : XMB01ParentTransition {

    #region Singleton
    public XMB01ParentMoveCompleteTransition() {

    }
    private static XMB01ParentMoveCompleteTransition instance = null;
    public static XMB01ParentMoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentMoveCompleteTransition();
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
