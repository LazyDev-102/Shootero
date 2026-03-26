using Class_FSM;
using UnityEngine;

public class XMB02DeadState : XMB02State {
    #region Singleton
    public XMB02DeadState() {

    }
    private static XMB02DeadState instance = null;
    public static XMB02DeadState Instance {
        get {
            if (instance == null) {
                instance = new XMB02DeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<XMB02Base> controller) {
    }

    protected override void DoStartActions(StateController<XMB02Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<XMB02Base> controller) {
    }

    protected override Transition<XMB02Base>[] GetTransitions() {
        return null;
    }
}
