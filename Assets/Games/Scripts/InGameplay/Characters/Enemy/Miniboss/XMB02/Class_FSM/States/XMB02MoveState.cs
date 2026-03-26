using Class_FSM;
using UnityEngine;

public class XMB02MoveState : XMB02State {

    #region Singleton
    public XMB02MoveState() {

    }
    private static XMB02MoveState instance = null;
    public static XMB02MoveState Instance {
        get {
            if (instance == null) {
                instance = new XMB02MoveState();
            }
            return instance;
        }
    }
    #endregion

    private XMB02Transition[] transitions = { XMB02MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<XMB02Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<XMB02Base> controller) {
        controller.ObjectBase.XMB02Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<XMB02Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.XMB02Move.MoveDirect();
    }

    protected override Transition<XMB02Base>[] GetTransitions() {
        return transitions;
    }
}
