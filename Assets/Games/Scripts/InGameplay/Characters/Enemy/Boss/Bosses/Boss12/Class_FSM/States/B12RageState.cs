

using Class_FSM;

public class B12RageState : B12State {

    #region Singleton
    public B12RageState() {

    }
    private static B12RageState instance = null;
    public static B12RageState Instance {
        get {
            if (instance == null) {
                instance = new B12RageState();
            }
            return instance;
        }
    }
    #endregion

    private B12Transition[] transitions = { B12EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B12Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B12Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B12Base> controller) {
        controller.ObjectBase.B12Attack.StartRage();
        controller.ObjectBase.B12Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B12Base> controller) {
    }

    protected override Transition<B12Base>[] GetTransitions() {
        return transitions;
    }
}
