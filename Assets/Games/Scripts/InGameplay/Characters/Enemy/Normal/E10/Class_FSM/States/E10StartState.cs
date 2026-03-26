

using Class_FSM;

public class E10StartState : E10State {
    #region Singleton
    public E10StartState() {

    }
    private static E10StartState instance = null;
    public static E10StartState Instance {
        get {
            if(instance == null) {
                instance = new E10StartState();
            }
            return instance;
        }
    }
    #endregion
    private E10Transition[] transitions = { E10CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<E10Base> controller) {
    }

    protected override void DoStartActions(StateController<E10Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E10Base> controller) {
    }

    protected override Transition<E10Base>[] GetTransitions() {
        return transitions;
    }
}
