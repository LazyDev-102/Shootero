

using Class_FSM;

public class B09PreRageState : B09State {
    #region Singleton
    public B09PreRageState() {

    }
    private static B09PreRageState instance = null;
    public static B09PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B09PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B09Transition[] transitions = { B09EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B09Base> controller) {
    }

    protected override void DoStartActions(StateController<B09Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B09Base> controller) {
        controller.ObjectBase.B09Move.KnockLooking();
    }

    protected override Transition<B09Base>[] GetTransitions() {
        return transitions;
    }

}
