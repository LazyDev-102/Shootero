

using Class_FSM;

public class B14PreRageState : B14State {
    #region Singleton
    public B14PreRageState() {

    }
    private static B14PreRageState instance = null;
    public static B14PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B14PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B14Transition[] transitions = { B14EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B14Base> controller) {
    }

    protected override void DoStartActions(StateController<B14Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B14Base> controller) {
        controller.ObjectBase.B14Move.KnockLooking();
    }

    protected override Transition<B14Base>[] GetTransitions() {
        return transitions;
    }
}
