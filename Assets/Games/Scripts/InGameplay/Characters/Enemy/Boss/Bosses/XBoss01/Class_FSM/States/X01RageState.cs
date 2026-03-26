

using Class_FSM;

public class XB01RageState : XB01State {

    #region Singleton
    public XB01RageState() {

    }
    private static XB01RageState instance = null;
    public static XB01RageState Instance {
        get {
            if (instance == null) {
                instance = new XB01RageState();
            }
            return instance;
        }
    }
    #endregion

    private XB01Transition[] transitions = { XB01EndRageTransition.Instance };
    protected override void DoEndActions(StateController<XB01Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.XB01Attack.EndRage();
    }

    protected override void DoStartActions(StateController<XB01Base> controller) {

        controller.ObjectBase.XB01Attack.StartRage();
        controller.ObjectBase.XB01Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<XB01Base> controller) {
    }

    protected override Transition<XB01Base>[] GetTransitions() {
        return transitions;
    }
}
