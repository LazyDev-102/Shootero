
using Class_FSM;

public class XMB02StartState : XMB02State {
    #region Singleton
    public XMB02StartState() {

    }
    private static XMB02StartState instance = null;
    public static XMB02StartState Instance {
        get {
            if (instance == null) {
                instance = new XMB02StartState();
            }
            return instance;
        }
    }
    #endregion

    private XMB02Transition[] transitions = { XMB02CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<XMB02Base> controller) {
    }

    protected override void DoStartActions(StateController<XMB02Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<XMB02Base> controller) {
    }

    protected override Transition<XMB02Base>[] GetTransitions() {
        return transitions;
    }
}
