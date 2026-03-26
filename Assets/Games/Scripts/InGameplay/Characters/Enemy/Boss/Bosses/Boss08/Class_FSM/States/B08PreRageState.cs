

using Class_FSM;

public class B08PreRageState : B08State {
    #region Singleton
    public B08PreRageState() {

    }
    private static B08PreRageState instance = null;
    public static B08PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B08PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B08Transition[] transitions = { B08EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B08Base> controller) {
    }

    protected override void DoStartActions(StateController<B08Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B08Base> controller) {
        controller.ObjectBase.B08Move.KnockLooking();
    }

    protected override Transition<B08Base>[] GetTransitions() {
        return transitions;
    }
}
