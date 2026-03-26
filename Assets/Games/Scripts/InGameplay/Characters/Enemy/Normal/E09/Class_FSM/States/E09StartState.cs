

using Class_FSM;

public class E09StartState : E09State {
    #region Singleton
    public E09StartState() {

    }
    private static E09StartState instance = null;
    public static E09StartState Instance {
        get {
            if(instance == null) {
                instance = new E09StartState();
            }
            return instance;
        }
    }
    #endregion
    private E09Transition[] transitions = { E09CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<E09Base> controller) {
    }

    protected override void DoStartActions(StateController<E09Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E09Base> controller) {
    }

    protected override Transition<E09Base>[] GetTransitions() {
        return transitions;
    }
}
