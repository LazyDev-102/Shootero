using Class_FSM;
using UnityEngine;

public class XMB01IdleState : XMB01State {
    #region Singleton
    public XMB01IdleState() {

    }
    private static XMB01IdleState instance = null;
    public static XMB01IdleState Instance {
        get {
            if (instance == null) {
                instance = new XMB01IdleState();
            }
            return instance;
        }
    }
    #endregion

    private XMB01Transition[] transitions = { XMB01CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<XMB01Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<XMB01Base> controller) {
    }

    protected override void DoUpdateActions(StateController<XMB01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<XMB01Base>[] GetTransitions() {
        return transitions;
    }
}
