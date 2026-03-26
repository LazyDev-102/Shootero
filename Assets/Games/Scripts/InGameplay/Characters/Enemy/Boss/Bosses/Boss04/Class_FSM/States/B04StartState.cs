

using Class_FSM;

public class B04StartState : B04State {
    #region Singleton
    public B04StartState() {

    }
    private static B04StartState instance = null;
    public static B04StartState Instance {
        get {
            if (instance == null) {
                instance = new B04StartState();
            }
            return instance;
        }
    }
    #endregion

    private B04Transition[] transitions = { B04CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<B04Base> controller) {
    }

    protected override void DoStartActions(StateController<B04Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return transitions;
    }
}
