
using Class_FSM;

public class XMB01StartState : XMB01State {
    #region Singleton
    public XMB01StartState() {

    }
    private static XMB01StartState instance = null;
    public static XMB01StartState Instance {
        get {
            if (instance == null) {
                instance = new XMB01StartState();
            }
            return instance;
        }
    }
    #endregion

    private XMB01Transition[] transitions = { XMB01CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<XMB01Base> controller) {
    }

    protected override void DoStartActions(StateController<XMB01Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<XMB01Base> controller) {
    }

    protected override Transition<XMB01Base>[] GetTransitions() {
        return transitions;
    }
}
