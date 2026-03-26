

using Class_FSM;

public class B13PreRageState : B13State {
    #region Singleton
    public B13PreRageState() {

    }
    private static B13PreRageState instance = null;
    public static B13PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B13PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B13Transition[] transitions = { B13EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B13Base> controller) {
    }

    protected override void DoStartActions(StateController<B13Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B13Base> controller) {
        controller.ObjectBase.B13Move.KnockLooking();
    }

    protected override Transition<B13Base>[] GetTransitions() {
        return transitions;
    }
}
