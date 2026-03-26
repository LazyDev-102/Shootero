
using Class_FSM;

public class B10PreRageState : B10State {
    #region Singleton
    public B10PreRageState() {

    }
    private static B10PreRageState instance = null;
    public static B10PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B10PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B10Transition[] transitions = { B10EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B10Base> controller) {
    }

    protected override void DoStartActions(StateController<B10Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {
        controller.ObjectBase.B10Move.KnockLooking();
    }

    protected override Transition<B10Base>[] GetTransitions() {
        return transitions;
    }
}
