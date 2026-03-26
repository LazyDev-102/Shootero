

using Class_FSM;

public class B02PreRageState : B02State {
    #region Singleton
    public B02PreRageState() {

    }
    private static B02PreRageState instance = null;
    public static B02PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B02PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B02Transition[] transitions = { B02EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B02Base> controller) {
    }

    protected override void DoStartActions(StateController<B02Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
        controller.ObjectBase.B02Move.KnockLooking();
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return transitions;
    }
}
