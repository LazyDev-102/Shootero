using Class_FSM;
using UnityEngine;

public class XMB01MoveState : XMB01State {

    #region Singleton
    public XMB01MoveState() {

    }
    private static XMB01MoveState instance = null;
    public static XMB01MoveState Instance {
        get {
            if (instance == null) {
                instance = new XMB01MoveState();
            }
            return instance;
        }
    }
    #endregion

    private XMB01Transition[] transitions = { XMB01MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<XMB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<XMB01Base> controller) {
        controller.ObjectBase.XMB01Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<XMB01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.XMB01Move.MoveDirect();
    }

    protected override Transition<XMB01Base>[] GetTransitions() {
        return transitions;
    }
}
