

using Class_FSM;

public class E15StartState : E15State {
    #region Singleton
    public E15StartState() {

    }
    private static E15StartState instance = null;
    public static E15StartState Instance {
        get {
            if(instance == null) {
                instance = new E15StartState();
            }
            return instance;
        }
    }
    #endregion
    private E15Transition[] transitions = { E15CanAppearTransition.Instance};
    protected override void DoEndActions(StateController<E15Base> controller) {
    }

    protected override void DoStartActions(StateController<E15Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E15Base> controller) {
    }

    protected override Transition<E15Base>[] GetTransitions() {
        return transitions;
    }
}
