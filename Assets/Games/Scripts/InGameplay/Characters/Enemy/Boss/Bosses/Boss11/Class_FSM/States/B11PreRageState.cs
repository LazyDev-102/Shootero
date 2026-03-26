

using Class_FSM;

public class B11PreRageState : B11State {
    #region Singleton
    public B11PreRageState() {

    }
    private static B11PreRageState instance = null;
    public static B11PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B11PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B11Transition[] transitions = { B11EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B11Base> controller) {
    }

    protected override void DoStartActions(StateController<B11Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B11Base> controller) {
        controller.ObjectBase.B11Move.KnockLooking();
    }

    protected override Transition<B11Base>[] GetTransitions() {
        return transitions;
    }
}
