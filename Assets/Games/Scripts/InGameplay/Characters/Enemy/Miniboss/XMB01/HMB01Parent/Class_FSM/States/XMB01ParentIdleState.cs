using Class_FSM;
using UnityEngine;

public class XMB01ParentIdleState : XMB01ParentState {
    #region Singleton
    public XMB01ParentIdleState() {

    }
    private static XMB01ParentIdleState instance = null;
    public static XMB01ParentIdleState Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentIdleState();
            }
            return instance;
        }
    }
    #endregion

    private XMB01ParentTransition[] transitions = { XMB01ParentCanAttackTransition.Instance };

    protected override void DoEndActions(StateController<XMB01ParentBase> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<XMB01ParentBase> controller) {
    }

    protected override void DoUpdateActions(StateController<XMB01ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<XMB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
