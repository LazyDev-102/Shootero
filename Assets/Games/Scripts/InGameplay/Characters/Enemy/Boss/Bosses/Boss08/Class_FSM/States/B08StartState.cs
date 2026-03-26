

using Class_FSM;

public class B08StartState : B08State {
    #region Singleton
    public B08StartState() {

    }
    private static B08StartState instance = null;
    public static B08StartState Instance {
        get {
            if (instance == null) {
                instance = new B08StartState();
            }
            return instance;
        }
    }
    #endregion
    private B08Transition[] transitions = { B08CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<B08Base> controller) {
    }

    protected override void DoStartActions(StateController<B08Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B08Base> controller) {
    }

    protected override Transition<B08Base>[] GetTransitions() {
        return transitions;
    }
}
