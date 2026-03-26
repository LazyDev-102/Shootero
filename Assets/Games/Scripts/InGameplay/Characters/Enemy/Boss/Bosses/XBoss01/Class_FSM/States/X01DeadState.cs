

using Class_FSM;

public class XB01DeadState : XB01State {
    #region Singleton
    public XB01DeadState() {

    }
    private static XB01DeadState instance = null;
    public static XB01DeadState Instance {
        get {
            if(instance == null) {
                instance = new XB01DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<XB01Base> controller) {
    }

    protected override void DoStartActions(StateController<XB01Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<XB01Base> controller) {
    }

    protected override Transition<XB01Base>[] GetTransitions() {
        return null;
    }
}
