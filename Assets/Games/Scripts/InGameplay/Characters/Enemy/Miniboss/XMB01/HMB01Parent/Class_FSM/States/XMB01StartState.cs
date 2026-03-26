
using Class_FSM;

public class XMB01ParentStartState : XMB01ParentState {
    #region Singleton
    public XMB01ParentStartState() {

    }
    private static XMB01ParentStartState instance = null;
    public static XMB01ParentStartState Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentStartState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<XMB01ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<XMB01ParentBase> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<XMB01ParentBase> controller) {
    }

    protected override Transition<XMB01ParentBase>[] GetTransitions() {
        return null;
    }
}
