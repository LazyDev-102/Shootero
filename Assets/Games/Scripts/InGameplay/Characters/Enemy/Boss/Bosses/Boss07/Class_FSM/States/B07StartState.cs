

using Class_FSM;

public class B07StartState : B07State {
    #region Singleton
    public B07StartState() {

    }
    private static B07StartState instance = null;
    public static B07StartState Instance {
        get {
            if (instance == null) {
                instance = new B07StartState();
            }
            return instance;
        }
    }
    #endregion

    private B07Transition[] transitions = { B07CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<B07Base> controller) {
    }

    protected override void DoStartActions(StateController<B07Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B07Base> controller) {
    }

    protected override Transition<B07Base>[] GetTransitions() {
        return transitions;
    }
}
