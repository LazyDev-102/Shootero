using Class_FSM;
using UnityEngine;

public class XMB01ParentMoveState : XMB01ParentState {

    #region Singleton
    public XMB01ParentMoveState() {

    }
    private static XMB01ParentMoveState instance = null;
    public static XMB01ParentMoveState Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentMoveState();
            }
            return instance;
        }
    }
    #endregion

    private XMB01ParentTransition[] transitions = { XMB01ParentMoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<XMB01ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<XMB01ParentBase> controller) {
        controller.ObjectBase.MinibossMove.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<XMB01ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MinibossMove.MoveDirect();
    }

    protected override Transition<XMB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
