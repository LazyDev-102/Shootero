

using Class_FSM;

public class B05PreRageState : B05State {
    #region Singleton
    public B05PreRageState() {

    }
    private static B05PreRageState instance = null;
    public static B05PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B05PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B05Transition[] transitions = { B05EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B05Base> controller) {
    }

    protected override void DoStartActions(StateController<B05Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B05Base> controller) {
        controller.ObjectBase.B05Move.KnockLooking();
    }

    protected override Transition<B05Base>[] GetTransitions() {
        return transitions;
    }
}
