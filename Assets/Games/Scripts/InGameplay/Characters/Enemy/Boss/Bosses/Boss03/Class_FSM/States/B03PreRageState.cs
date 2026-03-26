

using Class_FSM;

public class B03PreRageState : B03State {
    #region Singleton
    public B03PreRageState() {

    }
    private static B03PreRageState instance = null;
    public static B03PreRageState Instance {
        get {
            if (instance == null) {
                instance = new B03PreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B03Transition[] transitions = { B03EndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
        controller.ObjectBase.B03Move.KnockLooking();
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
