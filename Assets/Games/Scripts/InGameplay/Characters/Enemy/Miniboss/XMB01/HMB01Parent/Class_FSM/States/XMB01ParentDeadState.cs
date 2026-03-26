using Class_FSM;
using UnityEngine;

public class XMB01ParentDeadState : XMB01ParentState {
    #region Singleton
    public XMB01ParentDeadState() {

    }
    private static XMB01ParentDeadState instance = null;
    public static XMB01ParentDeadState Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentDeadState();
            }
            return instance;
        }
    }
    #endregion


    protected override void DoEndActions(StateController<XMB01ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<XMB01ParentBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<XMB01ParentBase> controller) {
    }

    protected override Transition<XMB01ParentBase>[] GetTransitions() {
        return null;
    }
}
