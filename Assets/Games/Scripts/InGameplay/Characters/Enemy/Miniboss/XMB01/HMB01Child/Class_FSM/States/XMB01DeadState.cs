using Class_FSM;
using UnityEngine;

public class XMB01DeadState : XMB01State {
    #region Singleton
    public XMB01DeadState() {

    }
    private static XMB01DeadState instance = null;
    public static XMB01DeadState Instance {
        get {
            if (instance == null) {
                instance = new XMB01DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<XMB01Base> controller) {
    }

    protected override void DoStartActions(StateController<XMB01Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<XMB01Base> controller) {
    }

    protected override Transition<XMB01Base>[] GetTransitions() {
        return null;
    }
}
