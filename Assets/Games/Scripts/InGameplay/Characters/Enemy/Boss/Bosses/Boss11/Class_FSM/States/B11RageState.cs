

using Class_FSM;

public class B11RageState : B11State {

    #region Singleton
    public B11RageState() {

    }
    private static B11RageState instance = null;
    public static B11RageState Instance {
        get {
            if (instance == null) {
                instance = new B11RageState();
            }
            return instance;
        }
    }
    #endregion

    private B11Transition[] transitions = { B11EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B11Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B11Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B11Base> controller) {
        controller.ObjectBase.B11Attack.StartRage();
        controller.ObjectBase.B11Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B11Base> controller) {
    }

    protected override Transition<B11Base>[] GetTransitions() {
        return transitions;
    }
}
