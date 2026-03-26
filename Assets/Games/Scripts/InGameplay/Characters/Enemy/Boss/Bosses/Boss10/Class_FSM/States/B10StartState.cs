

using Class_FSM;

public class B10StartState : B10State {
    #region Singleton
    public B10StartState() {

    }
    private static B10StartState instance = null;
    public static B10StartState Instance {
        get {
            if (instance == null) {
                instance = new B10StartState();
            }
            return instance;
        }
    }
    #endregion
    private B10Transition[] transtions = { B10CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<B10Base> controller) {
    }

    protected override void DoStartActions(StateController<B10Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {
    }

    protected override Transition<B10Base>[] GetTransitions() {
        return transtions;
    }
}
