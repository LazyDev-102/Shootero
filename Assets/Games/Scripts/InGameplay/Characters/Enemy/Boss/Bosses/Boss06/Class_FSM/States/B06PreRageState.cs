

using Class_FSM;

public class B06PreRageState : B06State {
    #region Singleton
    public B06PreRageState() {

    }
    private static B06PreRageState instance = null;
    public static B06PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B06PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B06Transition[] transitions = { B06EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B06Base> controller) {
    }

    protected override void DoStartActions(StateController<B06Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B06Base> controller) {
        controller.ObjectBase.B06Move.KnockLooking();
    }

    protected override Transition<B06Base>[] GetTransitions() {
        return transitions;
    }
}
