using Class_FSM;
using UnityEngine;

public class XMB02IdleState : XMB02State {
    #region Singleton
    public XMB02IdleState() {

    }
    private static XMB02IdleState instance = null;
    public static XMB02IdleState Instance {
        get {
            if (instance == null) {
                instance = new XMB02IdleState();
            }
            return instance;
        }
    }
    #endregion

    private XMB02Transition[] transitions = { XMB02CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<XMB02Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<XMB02Base> controller) {
    }

    protected override void DoUpdateActions(StateController<XMB02Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<XMB02Base>[] GetTransitions() {
        return transitions;
    }
}
