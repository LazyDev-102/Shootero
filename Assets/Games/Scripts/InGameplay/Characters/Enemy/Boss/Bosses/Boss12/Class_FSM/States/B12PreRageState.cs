

using Class_FSM;

public class B12PreRageState : B12State {
    #region Singleton
    public B12PreRageState() {

    }
    private static B12PreRageState instance = null;
    public static B12PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B12PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B12Transition[] transitions = { B12EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B12Base> controller) {
    }

    protected override void DoStartActions(StateController<B12Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B12Base> controller) {
        controller.ObjectBase.B12Move.KnockLooking();
    }

    protected override Transition<B12Base>[] GetTransitions() {
        return transitions;
    }
}
