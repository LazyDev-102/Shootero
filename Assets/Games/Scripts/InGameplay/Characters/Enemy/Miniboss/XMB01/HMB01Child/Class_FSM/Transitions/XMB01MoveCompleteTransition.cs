using Class_FSM;
using UnityEngine;

public class XMB01MoveCompleteTransition : XMB01Transition {

    #region Singleton
    public XMB01MoveCompleteTransition() {

    }
    private static XMB01MoveCompleteTransition instance = null;
    public static XMB01MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01MoveCompleteTransition();
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
