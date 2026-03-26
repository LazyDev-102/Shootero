

using Class_FSM;

public class B07PreRageState : B07State {
    #region Singleton
    public B07PreRageState() {

    }
    private static B07PreRageState instance = null;
    public static B07PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B07PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B07Transition[] transitions = { B07EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B07Base> controller) {
    }

    protected override void DoStartActions(StateController<B07Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B07Base> controller) {
        controller.ObjectBase.B07Move.KnockLooking();
    }

    protected override Transition<B07Base>[] GetTransitions() {
        return transitions;
    }
}
