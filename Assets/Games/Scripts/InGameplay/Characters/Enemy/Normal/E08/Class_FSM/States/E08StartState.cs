

using Class_FSM;

public class E08StartState : E08State {
    #region Singleton
    public E08StartState() {

    }
    private static E08StartState instance = null;
    public static E08StartState Instance {
        get {
            if(instance == null) {
                instance = new E08StartState();
            }
            return instance;
        }
    }
    #endregion
    private E08Transition[] transitions = { E08CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<E08Base> controller) {
    }

    protected override void DoStartActions(StateController<E08Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E08Base> controller) {
    }

    protected override Transition<E08Base>[] GetTransitions() {
        return transitions;
    }
}
