

using Class_FSM;

public class XB01StartEffectRageState : XB01State {
    #region Singleton
    public XB01StartEffectRageState() {

    }
    private static XB01StartEffectRageState instance = null;
    public static XB01StartEffectRageState Instance {
        get {
            if (instance == null) {
                instance = new XB01StartEffectRageState();
            }
            return instance;
        }
    }
    #endregion
    private XB01Transition[] transitions = { XB01EndEffectRageTransition.Instance };
    protected override void DoEndActions(StateController<XB01Base> controller) {
    }

    protected override void DoStartActions(StateController<XB01Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<XB01Base> controller) {
        controller.ObjectBase.XB01Move.KnockLooking();
    }

    protected override Transition<XB01Base>[] GetTransitions() {
        return transitions;
    }
}
