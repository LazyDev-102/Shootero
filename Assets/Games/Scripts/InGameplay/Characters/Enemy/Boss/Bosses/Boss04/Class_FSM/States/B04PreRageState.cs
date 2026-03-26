

using Class_FSM;

public class B04PreRageState : B04State {
    #region Singleton
    public B04PreRageState() {

    }
    private static B04PreRageState instance = null;
    public static B04PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B04PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B04Transition[] transitions = { B04EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B04Base> controller) {
    }

    protected override void DoStartActions(StateController<B04Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
        controller.ObjectBase.B04Move.KnockLooking();
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return transitions;
    }
}
