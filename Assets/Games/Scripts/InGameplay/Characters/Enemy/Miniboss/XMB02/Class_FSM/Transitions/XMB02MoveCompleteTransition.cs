using Class_FSM;
using UnityEngine;

public class XMB02MoveCompleteTransition : XMB02Transition {

    #region Singleton
    public XMB02MoveCompleteTransition() {

    }
    private static XMB02MoveCompleteTransition instance = null;
    public static XMB02MoveCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB02MoveCompleteTransition();
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
