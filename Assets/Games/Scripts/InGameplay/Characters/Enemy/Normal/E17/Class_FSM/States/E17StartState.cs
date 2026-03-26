

using Class_FSM;

public class E17StartState : E17State {
    #region Singleton
    public E17StartState() {

    }
    private static E17StartState instance = null;
    public static E17StartState Instance {
        get {
            if (instance == null) {
                instance = new E17StartState();
            }
            return instance;
        }
    }
    #endregion
    private E17Transition[] transitions = { E17CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<E17Base> controller) {
    }

    protected override void DoStartActions(StateController<E17Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E17Base> controller) {
    }

    protected override Transition<E17Base>[] GetTransitions() {
        return transitions;
    }
}
